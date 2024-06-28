using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{


    public int hitPoints;
    public int maxHitPoints;

    public float maxSpeed;

    public float acceleration;
    public float decelerationFactor;
    //public float bodyDrag;
    public float jumpForce;
    public float jumpMaxTime;
    public float jumpCooldown;
    public float attackCooldown;
    public float invulnerableCooldown;

    private bool isJumping = false;
    private float jumpTimer = 0;

    public float rideHeight;

    [SerializeField]
    private float rideSpringStrength;
    [SerializeField]
    private float rideSpringDamper;

    [SerializeField]
    private LayerMask isGround;
    [SerializeField]
    private float playerHeight;
    //[SerializeField]
    //private Animator anim;

    public GameObject attackHitbox;
    public GameObject playerBody;


    public Player player;
    [HideInInspector]
    public InputAction movement;
    private GameObject closestNPC;

    private bool isGrounded;
    private bool jumpReady = true;
    [HideInInspector]
    public bool canMove = true;
    public bool isInvulnerable;

    private bool isAttacking = false;

    private const float g = 9.81f;
    private ConstantForce gravity;

    [SerializeField]
    private GameObject quackprefab;
    [SerializeField]
    private Transform quackexit;

    private Rigidbody rb;


    private void Awake()
    {
        gravity = GetComponent<ConstantForce>();
        player = new Player();
        rb = GetComponentInChildren<Rigidbody>();

        gravity.force = new Vector3(0, -g * rb.mass, 0);
        isInvulnerable = false;
        attackHitbox.GetComponent<BoxCollider>().enabled = false;
        attackHitbox.SetActive(false);

        movement = player.main.Movement;
    }

    private void Update()
    {

        //Quack();
        Movement();
        Jump();
        //SpeedControl();
        GroundCheck();
    }

    private void Movement()
    {

        if (canMove)
        {
            Vector3 move = new Vector3(movement.ReadValue<Vector2>().x, 0f, movement.ReadValue<Vector2>().y);

            if (move != Vector3.zero)
            {
                //anim.SetBool("moving", true);
                //Vector3 goalVel = move * maxSpeed;
                //Vector3 curVel = rb.velocity;

                //Vector3 deltaVel = curVel - goalVel;

                //Vector3 accel = deltaVel.normalized * (acceleration * Time.fixedDeltaTime);

                //if (accel.sqrMagnitude > deltaVel.sqrMagnitude) {
                //    accel = deltaVel;
                //}

                rb.AddForce(move * maxSpeed);

                if (rb.velocity.x * rb.velocity.x > maxSpeed * maxSpeed || rb.velocity.z * rb.velocity.z > maxSpeed * maxSpeed)
                {
                    Vector3 clamp = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
                    rb.velocity = new Vector3(clamp.x, rb.velocity.y, clamp.z);
                }

                //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

            }
            else
            {
                Vector3 decel = -rb.velocity.normalized * (decelerationFactor * Time.fixedDeltaTime);
                rb.AddForce(decel * rb.mass);
                //anim.SetBool("moving", false);

                if (Vector3.Dot(rb.velocity, decel) < 0f)
                {
                    rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
                }
            }
        }
    }

    
    
    private void Jump()
    {

        //Debug.Log(jumpReady);
        if (isJumping)
        {
            isGrounded = false;
            jumpTimer += Time.deltaTime;
            float finalJumpForce = jumpForce / (1f + jumpTimer);
            //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * finalJumpForce, ForceMode.Force);
            
        }
        if (jumpTimer >= jumpMaxTime || (jumpTimer < jumpMaxTime && jumpTimer > 0f && !isJumping))
        {
            isJumping = false;
            jumpTimer = 0f;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        
        

        
    }

    public void OnJumpStart(InputAction.CallbackContext context)
    {
        if (jumpReady && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpReady = false;
            isJumping = true;
            //rb.drag = bodyDrag;
            //Jump();

            //Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    public void OnJumpCancel(InputAction.CallbackContext context)
    {
        isJumping = false;
        jumpTimer = 0f;
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void GroundCheck()
    {

        isGrounded = Physics.Linecast(transform.position, transform.position - new Vector3(0f, rideHeight, 0f), isGround, QueryTriggerInteraction.Ignore);
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, rideHeight, isGround, QueryTriggerInteraction.Ignore);

        if (isGrounded)
        {
            
            Vector3 vel = rb.velocity;
            Vector3 rayDir = transform.TransformDirection(Vector3.down);

            Vector3 otherVel = Vector3.zero;
            Rigidbody hitBody = hit.rigidbody;
            if (hitBody != null)
            {
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



    private void ResetJump()
    {
        jumpReady = true;
    }

    IEnumerator Attack()
    {
        attackHitbox.SetActive(true);
        attackHitbox.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(0.3f);
        attackHitbox.GetComponent<BoxCollider>().enabled = false;
        attackHitbox.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    #region EnableDisable

    private void OnEnable()
    {
        player.main.Enable();

        player.main.Jump.performed += OnJumpStart;
        player.main.Jump.canceled += OnJumpCancel;
        player.main.Attack.performed += OnAttack;
        player.main.Quack.performed += OnQuack;
    }

    private void OnDisable()
    {
        player.main.Disable();
    }

    #endregion

    

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }
    }

    public void OnQuack(InputAction.CallbackContext context)
    {
        Instantiate(quackprefab, quackexit.position, Quaternion.identity, transform);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        closestNPC.GetComponent<NPC>().Dialogue();
    }



    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            StartCoroutine(CycleMovement());
            hitPoints -= damage;
            Invoke(nameof(ResetVulnerable), invulnerableCooldown);
        }
    }

    public void ResetVulnerable()
    {
        isInvulnerable = false;
    }

    IEnumerator CycleMovement()
    {
        canMove = false;
        yield return new WaitForSeconds(1);
        canMove = true;
    }

    public void ToggleMovement()
    {
        canMove = !canMove;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC Dialogue"))
        {
            player.main.Quack.performed -= OnQuack;
            player.main.Quack.performed += OnInteract;
            closestNPC = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC Dialogue"))
        {
            player.main.Quack.performed -= OnInteract;
            player.main.Quack.performed += OnQuack;
        }
    }

    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - rideHeight, transform.position.z));
    }


    #endregion

}
