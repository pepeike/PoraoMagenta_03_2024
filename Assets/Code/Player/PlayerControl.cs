using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour {


    public int hitPoints;
    public int maxHitPoints;
    
    public float maxSpeed;
    
    public float acceleration;
    public float decelerationFactor;
    //public float bodyDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float attackCooldown;
    public float invulnerableCooldown;

    public float rideHeight;

    [SerializeField]
    private float rideSpringStrength;
    [SerializeField]
    private float rideSpringDamper;

    [SerializeField]
    private LayerMask isGround;
    [SerializeField]
    private float playerHeight;

    public GameObject attackHitbox;
    public GameObject playerBody;
    

    private Player player;
    [HideInInspector]
    public InputAction movement;
    private GameObject closestNPC;

    private bool isGrounded;
    private bool jumpReady = true;
    [HideInInspector]
    public bool canMove = true;
    private bool isInvulnerable;

    private bool isAttacking = false;

    private const float g = 9.81f;
    private ConstantForce gravity;

    [SerializeField]
    private GameObject quackprefab;
    [SerializeField]
    private Transform quackexit;

    private Rigidbody rb;


    private void Awake() {
        gravity = GetComponent<ConstantForce>();
        player = new Player();
        rb = GetComponentInChildren<Rigidbody>();

        gravity.force = new Vector3(0, -g * rb.mass, 0);
        isInvulnerable = false;

        movement = player.main.Movement;
    }

    private void Update() {

        Quack();
        Movement();
        //SpeedControl();
        GroundCheck();
    }

    private void Movement() {

        if (canMove) {
            Vector3 move = new Vector3(movement.ReadValue<Vector2>().x, 0f, movement.ReadValue<Vector2>().y);

            if (move != Vector3.zero) {
                Vector3 goalVel = move * maxSpeed;
                Vector3 curVel = rb.velocity;

                Vector3 deltaVel = curVel - goalVel;

                Vector3 accel = deltaVel.normalized * (acceleration * Time.fixedDeltaTime);

                if (accel.sqrMagnitude > deltaVel.sqrMagnitude) {
                    accel = deltaVel;
                }

                rb.AddForce(-accel * rb.mass);

                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

            } else {
                Vector3 decel = -rb.velocity.normalized * (decelerationFactor * Time.fixedDeltaTime);
                rb.AddForce(decel * rb.mass);

                if (Vector3.Dot(rb.velocity, decel) < 0f) {
                    rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
                }
            }
        }
    }

    

    private void Jump() {

        if (isGrounded) {

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        }
    }

    private void Quack()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Instantiate(quackprefab,quackexit.position, Quaternion.identity);
        }
    }

    private void GroundCheck() {

        isGrounded = Physics.Linecast(transform.position, transform.position - new Vector3(0f, rideHeight, 0f), isGround, QueryTriggerInteraction.Ignore);
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, rideHeight, isGround, QueryTriggerInteraction.Ignore);

        if (isGrounded) {

            Vector3 vel = rb.velocity;
            Vector3 rayDir = transform.TransformDirection(Vector3.down);

            Vector3 otherVel = Vector3.zero;
            Rigidbody hitBody = hit.rigidbody;
            if (hitBody != null) {
                otherVel = hitBody.velocity;
            }
            float rayDirVel = Vector3.Dot(rayDir, vel);
            float otherDirVel = Vector3.Dot(rayDir, vel);

            float relVel = rayDirVel - otherDirVel;

            float x = hit.distance - rideHeight;

            float springForce = (x * rideSpringStrength) - (relVel * rideSpringDamper);

            rb.AddForce(rayDir * springForce);

        }
    }

    

    private void ResetJump() {
        jumpReady = true;
    }

    IEnumerator Attack() {
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        attackHitbox.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    #region EnableDisable

    private void OnEnable() {
        player.main.Enable();

        player.main.Jump.performed += OnJump;
        player.main.Attack.performed += OnAttack;
    }

    private void OnDisable() {
        player.main.Disable();
    }

    #endregion

    public void OnJump(InputAction.CallbackContext context) {
        if (jumpReady && isGrounded) {
            jumpReady = false;
            //rb.drag = bodyDrag;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    public void OnAttack(InputAction.CallbackContext context) {
        if (!isAttacking) {
            isAttacking = true;
            StartCoroutine(Attack());
        }
    }

    public void OnQuack(InputAction.CallbackContext context) {

    }

    public void OnInteract(InputAction.CallbackContext context) {
        closestNPC.GetComponent<NPC>().Dialogue();
    }

    

    public void TakeDamage(int damage) {
        if (!isInvulnerable) {
            isInvulnerable = true;
            StartCoroutine(CycleMovement());
            hitPoints -= damage;
            Invoke(nameof(ResetVulnerable), invulnerableCooldown);
        }
    }

    public void ResetVulnerable() {
        isInvulnerable = false;
    }

    IEnumerator CycleMovement() {
        canMove = false;
        yield return new WaitForSeconds(1);
        canMove = true;
    }

    public void ToggleMovement() {
        canMove = !canMove;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("NPC Dialogue")) {
            player.main.Quack.performed -= OnQuack;
            player.main.Quack.performed += OnInteract;
            closestNPC = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("NPC Dialogue")) {
            player.main.Quack.performed -= OnInteract;
            player.main.Quack.performed += OnQuack;
        }
    }

    #region Gizmos

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - rideHeight, transform.position.z));
    }


    #endregion

}
