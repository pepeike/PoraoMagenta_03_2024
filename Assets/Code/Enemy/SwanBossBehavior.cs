using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwanBossBehavior : MonoBehaviour {


    
    [SerializeField]
    private float moveAttackSpeed;
    [SerializeField]
    private float returnToCenterSpeed;
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private int damage;

    private bool gotPlayerDir = false;
    private bool startedAtk = false;
    public bool isInvulnerable = false;
    private short moveAtkCounter = 0;
    private float flyAtkTimer = 0f;
    private bool didDamage = false;

    private enum State {
        idle,
        inter,
        moveAttack,
        wingAttack,
        flyAttack
    }

    private State currentState;
    private State prevState;

    private Vector3 moveDir;
    private Vector3 camDir = new Vector3(0, 0, -1);

    public LayerMask isBound;

    public GameObject player;
    public GameObject orient;
    public GameObject projectile;
    public GameObject charge;
    public Transform projectileSpawn;
    public Transform centerStage;
    private Rigidbody rb;


    private void Awake() {
        //orient.transform.localPosition = transform.forward;
        rb = GetComponent<Rigidbody>();
        currentState = State.idle;
        prevState = State.flyAttack;
        charge.SetActive(false);

        GetComponent<BossLife>().OnZeroLife += OnDeath;
    }

    private void OnDeath(object sender, EventArgs e) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update() {
        //Debug.Log(currentState);
        //orient.transform.localPosition = transform.forward;
        StateMachine();
    }

    private void StateMachine() {
        switch (currentState) {
            case State.idle:
                Idle();
                break;
            case State.moveAttack:
                MoveAtk();
                break;
            case State.wingAttack:
                WingAttack();
                break;
            case State.flyAttack:
                FlyAttack();
                break;
            case State.inter:
                ReturnToCenter();
                break;
        }
    }

    private void MoveAtk() {

        if (!gotPlayerDir) {
            moveDir = GetPlayerDir();
            charge.SetActive(true);
            //Debug.Log(playerDir);

            gotPlayerDir = true;
        }
        if (gotPlayerDir) {
            Invoke(nameof(MoveAtkProcess), 1f);
            bool hit = Physics.Raycast(transform.position, transform.forward, 2f, isBound, QueryTriggerInteraction.Collide);
            isInvulnerable = true;
            if (hit) {
                gotPlayerDir = false;
                moveAtkCounter++;
                //Debug.Log(moveAtkCounter);
            }

            if (moveAtkCounter > 5) {
                charge.SetActive(false);
                moveAtkCounter = 0;
                prevState = State.moveAttack;
                currentState = State.inter;
            }
        }

    }

    private void MoveAtkProcess() {
        rb.velocity = moveDir * moveAttackSpeed;
    }

    private void ReturnToCenter() {
        float distanceToCenter = Vector3.SqrMagnitude(centerStage.position - transform.position);
        gotPlayerDir = false;
        startedAtk = false;
        moveDir = GetCenterDir();
        transform.forward = moveDir;
        rb.velocity = moveDir * returnToCenterSpeed;
        if (distanceToCenter <= 10f) {
            transform.forward = camDir;
            rb.velocity = Vector3.zero;
            isInvulnerable = false;
            currentState = State.idle;
        }
    }

    private void WingAttack() {
        if (!gotPlayerDir) {
            moveDir = GetPlayerDir();
            gotPlayerDir = true;
        } else if (gotPlayerDir) {
            if (!startedAtk) {
                startedAtk = true;
                prevState = State.wingAttack;
                StartCoroutine(WingAtkWindup());
            }
        }
    }

    private void InstantiateWindAtk(Transform pos) {
        GameObject proj = Instantiate(projectile, pos.position, Quaternion.LookRotation(transform.forward, Vector3.up));
        proj.GetComponent<SwanBossProjectile>().projectileSpeed = projectileSpeed;
    }
    private void InstantiateWindAtk(Vector3 pos, float spd) {
        GameObject proj = Instantiate(projectile, pos, Quaternion.LookRotation(transform.forward, Vector3.up));
        proj.GetComponent<SwanBossProjectile>().projectileSpeed = spd;
    }

    private IEnumerator WingAtkWindup() {
        yield return new WaitForSeconds(0.5f);
        InstantiateWindAtk(projectileSpawn);
        yield return new WaitForSeconds(1f);
        gotPlayerDir = false;
        currentState = State.inter;
    }

    private bool flying = false;
    private bool flyingAtkEnded = false;
    public Transform projectileSpawn2;
    public LayerMask isGround;

    private void FlyAttack() {
        if (!flying && !flyingAtkEnded) {
            transform.forward = GetPlayerDir();
            float distanceToCenter = Vector3.SqrMagnitude(centerStage.position - transform.position);
            rb.velocity = Vector3.up * 4f;
            if (distanceToCenter > 50f) {
                flying = true;
            }
        } else if (flying && !flyingAtkEnded) {
            rb.useGravity = false;
            flyAtkTimer += Time.deltaTime;
            float a = Mathf.Sin(flyAtkTimer);
            float b = Mathf.Cos(flyAtkTimer);
            rb.velocity = new Vector3(a * 10 - 2, 0f, -b * 5);
            if (flyAtkTimer > 10) {
                rb.velocity = Vector3.zero;
                if (flyAtkTimer > 11) {
                    flying = false;
                    flyAtkTimer = 0f;
                    flyingAtkEnded = true;
                    rb.useGravity = true;
                    rb.AddForce(Vector3.down * 10f, ForceMode.Impulse);
                }
            }
        }
        if (!flying && flyingAtkEnded) {
            bool hitGround = Physics.Raycast(transform.position, Vector3.down, 2f, isGround);
            transform.forward = GetPlayerDir();
            if (hitGround) {
                rb.velocity = Vector3.zero;
                transform.forward = GetPlayerDir();
                InstantiateWindAtk(projectileSpawn.position, projectileSpeed);
                InstantiateWindAtk(projectileSpawn2.position, -projectileSpeed);
                flying = false;
                flyingAtkEnded = false;
                currentState = State.inter;
                prevState = State.flyAttack;
            }
        }
    }

    private Vector3 GetPlayerDir() {
        Vector3 _playerDir = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z).normalized;
        transform.forward = _playerDir;
        return _playerDir;
    }

    private Vector3 GetCenterDir() {
        Vector3 _centerDir = new Vector3(centerStage.position.x - transform.position.x, 0f, centerStage.position.z - transform.position.z).normalized;
        transform.forward = _centerDir;
        return _centerDir;
    }

    private bool startingNewAtk = false;

    private void Idle() {
        switch (prevState) {
            case State.moveAttack:
                if (!startingNewAtk) {
                    startingNewAtk = true;
                    StartCoroutine(StartNewAtk(State.wingAttack));
                }
                break;
            case State.wingAttack:
                if (!startingNewAtk) {
                    startingNewAtk = true;
                    StartCoroutine(StartNewAtk(State.flyAttack));
                }
                break;
            case State.flyAttack:
                if (!startingNewAtk) {
                    startingNewAtk = true;
                    StartCoroutine(StartNewAtk(State.moveAttack));
                }
                break;
            default:
                currentState = State.moveAttack;
                break;
        }
    }

    private IEnumerator StartNewAtk(State state) {
        yield return new WaitForSeconds(2f);
        currentState = state;
        startingNewAtk = false;
    }

    private IEnumerator ResetDamage() {
        yield return new WaitForSeconds(1f);
        didDamage = false;
    }

    private void OnTriggerStay(Collider other) {
        if (!didDamage && other.gameObject.CompareTag("Player")) {
            didDamage = true;
            other.GetComponentInParent<PlayerControl>().TakeDamage(damage);
            StartCoroutine(ResetDamage());
        }
    }

    private void OnDrawGizmos() {
        //Gizmos.DrawSphere(transform.position + Vector3.down * transform.localScale.y, 2f);
    }
}
