using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{

    [SerializeField]
    private int life;
    //[SerializeField]
    //private Animator anim;
    public void TakeDamage(int damage) {
        if (life > 1) {
            life -= damage;
        } else {
            //anim.SetTrigger("died");
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
    }


}
