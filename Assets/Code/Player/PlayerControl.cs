using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour {

    public float moveSpeed;
    public float maxSpeed;
    public float speedFactor;
    public AnimationCurve AccelerationFactorFromDot;
    public float acceleration;
    //public float bodyDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float attackCooldown;

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
    

    private bool isGrounded;
    private bool jumpReady = true;

    private bool isAttacking = false;

    private const float g = 9.81f;
    private ConstantForce gravity;
    

    private Rigidbody rb;


    private void Awake() {
        gravity = GetComponent<ConstantForce>();
        gravity.force = new Vector3(0, -g, 0);
        
        player = new Player();
        rb = GetComponentInChildren<Rigidbody>();
        movement = player.main.Movement;
    }

    private void FixedUpdate() {
        
        
        Movement();
        //SpeedControl();
        GroundCheck();
    }

    private void Movement() {

        //rb.drag = bodyDrag;
        Vector2 move = new Vector3(movement.ReadValue<Vector2>().x, 0f, movement.ReadValue<Vector2>().y);
        //Vector3 unitGoal = new Vector3(move.x, 0f, move.y);
        Vector3 goalVel = move * maxSpeed * speedFactor;
        Vector3 m_goalVel = move * maxSpeed;

        // Read input
        // Take current velocity
        // Take goal velocity
        // Calculate needed acceleration in frame to reach goal velocity


        //float accel = acceleration * AccelerationFactorFromDot.
        //rb.AddForce(new Vector3(mov.x * moveSpeed, 0, mov.y * moveSpeed));
        
    }

    

    private void Jump() {

        if (isGrounded) {

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

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

    private void OnEnable() {
        player.main.Enable();

        player.main.Jump.performed += OnJump;
        player.main.Attack.performed += OnAttack;
    }

    private void OnDisable() {
        player.main.Disable();
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (jumpReady && isGrounded) {
            jumpReady = false;
            //rb.drag = bodyDrag;
            Jump();

            Invoke("ResetJump", jumpCooldown);
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

    }


    #region Gizmos

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - rideHeight, transform.position.z));
    }


    #endregion

}
