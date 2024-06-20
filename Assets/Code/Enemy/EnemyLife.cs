using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{

    [SerializeField]
    private int life;

    public void TakeDamage(int damage) {
        if (life > 1) {
            life -= damage;
        } else {
            Destroy(gameObject);
        }
    }


}
