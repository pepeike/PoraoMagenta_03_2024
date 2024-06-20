using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    private bool didDamage = false;
    [SerializeField]
    private int damage;



    private void OnDisable() {
        didDamage = false;
    }

    private void OnEnable() {
        didDamage = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (!didDamage && other.gameObject.CompareTag("Player")) {
            Debug.Log("Damage");
            didDamage = true;
            other.GetComponentInParent<PlayerControl>().TakeDamage(damage);
            other.GetComponentInParent<Rigidbody>().AddForce((other.transform.position - transform.position).normalized * 2f, ForceMode.Impulse);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (!didDamage && other.gameObject.CompareTag("Player")) {
            didDamage = true;
            other.GetComponentInParent<PlayerControl>().TakeDamage(damage);
        }
    }

}
