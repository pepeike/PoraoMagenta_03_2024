using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float enemySpeed;
    private Rigidbody rb;
    private GameObject target;
    private float distanceToTarget;

    private bool isAttacking;

    public enum State {
        Idle,
        Aggro,
        ReadyToAttack
    }
    private State currentstate;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        currentstate = State.Idle;
        isAttacking = false;
    }
    void Update() {

        StateMachine();

        
    }

    private void StateMachine() {
        switch (currentstate) {
            case State.Idle:
                break;
            case State.Aggro:
                Vector3 playerEnemyOffset = target.transform.position - transform.position;
                distanceToTarget = Vector3.SqrMagnitude(playerEnemyOffset);
                //Debug.Log(distanceToTarget);
                rb.velocity = playerEnemyOffset.normalized * enemySpeed;
                if (distanceToTarget <= maxDistance * maxDistance) {
                    Debug.Log("Changing State");
                    currentstate = State.ReadyToAttack;
                }
                break;
            case State.ReadyToAttack:
                rb.velocity = Vector3.zero;
                Debug.Log("GettingReadyToAttack!");
                if (!isAttacking) {
                    isAttacking = true;
                    StartCoroutine(AttackWindUp());
                }
                break;
        }
    }
    


    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            //currentstate = State.ReadyToAttack;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            target = other.gameObject;
            currentstate = State.Aggro;
            //Debug.Log(currentstate + " " + target);
        }
    }

    IEnumerator AttackWindUp() {

        yield return new WaitForSeconds(3);
        Attack();
    }

    private void Attack() {
        Debug.Log("Attacked");
        ReturnToAggro();
    }

    private void ReturnToAggro() {
        currentstate = State.Aggro;
        isAttacking = false;
    }
}
