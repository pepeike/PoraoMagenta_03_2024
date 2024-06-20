using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
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
        if (!didDamage && other.gameObject.CompareTag("Enemy Hitbox")) {
            Debug.Log("Damage");
            didDamage = true;
            other.GetComponent<EnemyLife>().TakeDamage(damage);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (!didDamage && other.gameObject.CompareTag("Enemy Hitbox")) {
            didDamage = true;
            other.GetComponent<EnemyLife>().TakeDamage(damage);
        }
    }

}
