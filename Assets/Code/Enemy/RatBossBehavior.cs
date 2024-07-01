using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RatBossBehavior : MonoBehaviour {
    [SerializeField]
    private float mainMoveSpeed;
    [SerializeField]
    private float chargeAttackSpeed;
    [SerializeField]
    private float returnToCenterSpeed;
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private int damage;

    private bool gotDir = false;
    private bool startedAtk = false;
    public bool isInvulnerable = false;
    private short mainAtkCounter = 0;
    private bool didDamage = false;
    
    public List<Transform> minionSpawns = new List<Transform>();
    private List<GameObject> minions = new List<GameObject>();

    private enum State {
        idle,
        inter,
        moveAttack,
        callAttack,
        chargeAttack,
        finalAttack
    }

    [SerializeField]
    private State currentState;
    private State prevState;
    private State nextState = State.moveAttack;

    private Vector3 moveDir;
    private Vector3 camDir = new Vector3(0, 0, -1);

    public LayerMask isBound;
    

    public GameObject player;
    //public GameObject orient;
    public GameObject atkHitbox;
    public GameObject minionPrefab;
    public Transform centerStage;
    public Transform leftSide;
    public Transform rightSide;
    private Rigidbody rb;
    private BossLife lifeScript;


    private void Awake() {
        //orient.transform.localPosition = transform.forward;
        rb = GetComponent<Rigidbody>();
        lifeScript = GetComponent<BossLife>();
        //currentState = State.idle;
        prevState = State.idle;

        lifeScript.OnZeroLife += EndSequence;
        
    }

    

    private void Update() {
        Debug.Log(startedAtk);
        //orient.transform.localPosition = transform.forward;
        StateMachine();
    }

    private void StateMachine() {
        switch (currentState) {
            case State.idle:
                Idle();
                break;
            case State.moveAttack:
                MainAtk();
                break;
            case State.callAttack:
                CallAttack();
                break;
            case State.chargeAttack:
                ChargeAttack();
                break;
            case State.inter:
                ReturnToCenter();
                break;
            case State.finalAttack:
                FinalAttack();
                break;
        }
    }

    private bool startingMainAtk = false;

    private void MainAtk() {

        if (mainAtkCounter > 4) {
            prevState = State.moveAttack;
            currentState = State.inter;
        }

        if (!startingMainAtk) {
            rb.velocity = GetPlayerDir() * mainMoveSpeed;
        }

        
        if (Vector3.Distance(player.transform.position, transform.position) <= 3f) {
            startingMainAtk = true;
            rb.velocity = Vector3.zero;
            if (!startedAtk) {
                StartCoroutine(MainAtkProcess());
                startedAtk = true;
            }
        }
    }

    private IEnumerator MainAtkProcess() {
        yield return new WaitForSeconds(1);
        atkHitbox.SetActive(true);
        atkHitbox.GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(1);
        atkHitbox.GetComponent<Collider>().enabled = false;
        atkHitbox.SetActive(false);
        startedAtk = false;
        startingMainAtk = false;
        mainAtkCounter++;
        
    }

    private void ReturnToCenter() {
        float distanceToCenter = Vector3.SqrMagnitude(centerStage.position - transform.position);
        gotDir = false;
        startedAtk = false;
        mainAtkCounter = 0;
        chargeFinished = false;
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

    private Vector3 goToPoint;
    private bool startingAtk = false;
    private float distToPoint;

    private void CallAttack() {
        isInvulnerable = true;
        
        
        if (!gotDir && !startedAtk) {
            goToPoint = GetFurthestPoint();
            distToPoint = Vector3.Distance(transform.position, goToPoint);
            moveDir = goToPoint - transform.position;
            
            gotDir = true;
        } else if (gotDir && !startedAtk && distToPoint > 3 && !startingAtk) {
            //Debug.Log(distToPoint);
            rb.velocity = moveDir.normalized * (chargeAttackSpeed * 1.5f);
            distToPoint = Vector3.Distance(transform.position, goToPoint);
            if (distToPoint < 3) {
                startingAtk = true;
            }
        } else if (!gotDir && startedAtk) {
            goToPoint = GetFurthestPoint();
            moveDir = goToPoint - transform.position;
            distToPoint = Vector3.Distance(transform.position, goToPoint);
            rb.velocity = moveDir.normalized * (chargeAttackSpeed * 1.5f);
            if (distToPoint < 3) {
                startingAtk = true;
                gotDir = true;
            }
        }

        

        if (startedAtk && gotDir) {
            float _dist = Vector3.Distance(transform.position, player.transform.position);
            Debug.Log(_dist);
            if (_dist < 4f) {
                
                gotDir = false;
            } else {
                for (int i = 0; i < minions.Count; i++) {
                    if (minions[i] == null) {
                        minions.Remove(minions[i]);
                    }
                }

                if (minions.Count == 0) {
                    prevState = State.callAttack;
                    currentState = State.inter;
                    isInvulnerable = false;
                }
            }
        }

        if (startingAtk) {
            Debug.Log("starting");
            rb.velocity = Vector3.zero;
            startingAtk = false;
            if (!startedAtk) {
                SpawnEnemies();
                startedAtk = true;
                
            }
        }
    }

    private void SpawnEnemies() {
        foreach(Transform t in minionSpawns) {
            minions.Add(Instantiate(minionPrefab, t.position, Quaternion.identity));
        }
    }

    private bool chargeStarted = false;
    private bool chargeFinished = false;

    private void ChargeAttack() {
        if (!gotDir) {
            moveDir = GetPlayerDir();
            gotDir = true;
            chargeFinished = false;
        }

        if (gotDir && !chargeFinished) {
            bool hit = Physics.Raycast(transform.position, transform.forward, 2f, isBound, QueryTriggerInteraction.Collide);
            isInvulnerable = true;
            if (!startedAtk) {
                StartCoroutine(ChargeAtkProcess());
                startedAtk = true;
            }

            if (chargeStarted) {
                rb.velocity = moveDir * chargeAttackSpeed;
            }

            if (hit) {
                rb.velocity = Vector3.zero;
                isInvulnerable = false;
                StartCoroutine(EndChargeAtk());
                chargeFinished = true;
            }
        }
    }

    private IEnumerator ChargeAtkProcess() {
        yield return new WaitForSeconds(1);
        chargeStarted = true;
    }

    private IEnumerator EndChargeAtk() {
        yield return new WaitForSeconds(5);
        prevState = State.chargeAttack;
        chargeStarted = false;
        chargeFinished = false;
        currentState = State.inter;
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

    private Vector3 GetFurthestPoint() {
        float _distToLeft = Vector3.SqrMagnitude(player.transform.position - leftSide.position);
        float _distToRight = Vector3.SqrMagnitude(player.transform.position - rightSide.position);
        if (_distToLeft > _distToRight) {
            return leftSide.position;
        } else {
            return rightSide.position;
        }
    }

    private bool startingNewAtk = false;

    private void Idle() {
        if (nextState == State.finalAttack) {
            StartCoroutine(StartNewAtk(State.finalAttack));
        } else {
            switch (prevState) {
                case State.moveAttack:
                    if (!startingNewAtk) {
                        startingNewAtk = true;
                        StartCoroutine(StartNewAtk(State.callAttack));
                    }
                    break;
                case State.callAttack:
                    if (!startingNewAtk) {
                        startingNewAtk = true;
                        StartCoroutine(StartNewAtk(State.chargeAttack));
                    }
                    break;
                case State.chargeAttack:
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
        
    }

    private bool endingFight = false;
    private void FinalAttack() {
        rb.velocity = GetPlayerDir() * chargeAttackSpeed;
        endingFight = true;
    }

    private void EndSequence(object sender, System.EventArgs e) {
        nextState = State.finalAttack;
        currentState = State.idle;
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

    private void EndFight() {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerStay(Collider other) {
        if (!didDamage && other.gameObject.CompareTag("Player")) {
            didDamage = true;
            other.GetComponentInParent<PlayerControl>().TakeDamage(damage);
            StartCoroutine(ResetDamage());
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player") && endingFight) {
            collision.gameObject.GetComponent<PlayerControl>().canMove = false;
            collision.gameObject.GetComponent<PlayerControl>().isInvulnerable = true;
            Invoke(nameof(EndFight), 3f);
        }
    }

    private void OnDrawGizmos() {
        //Gizmos.DrawSphere(transform.position + Vector3.down * transform.localScale.y, 2f);
    }
}
