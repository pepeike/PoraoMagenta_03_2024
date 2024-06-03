using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class EnemyScript : MonoBehaviour
{

    public GameObject target;
    public enum State {Aggro,ReadyToAttack}
    public State currentstate;
    private void Start()
    {
        currentstate = State.Aggro;
    }
    void Update()
    {
        if (currentstate == State.Aggro)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.003f);
        }
       
        if (currentstate == State.ReadyToAttack)
        {

            Debug.Log("GettingReadyToAttack!");
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentstate=State.ReadyToAttack;
        }
    }
    
    IEnumerator AttackWindUp()
    {

        yield return new WaitForSeconds(3);
        Attack();
    }

    private void Attack()
    {
        Debug.Log("Attacked");
        ReturnToAggro();
    }

    private void ReturnToAggro()
    {
        currentstate = State.Aggro;
    }
}
