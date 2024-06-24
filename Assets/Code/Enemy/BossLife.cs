using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLife : MonoBehaviour
{

    [SerializeField]
    private int life;
    [SerializeField]
    private SwanBossBehavior swanBoss;
    
    private bool isInvulnerable;

    private void Update() {
        if (swanBoss != null) {
            isInvulnerable = swanBoss.isInvulnerable;
        } else {
            // Todo: isInvulnerable do boss rato
        }
    }

    public void TakeDamage(int damage) {
        if (!isInvulnerable)
        {
            if (life > 1) {
                life -= damage;
            } else {
                Destroy(gameObject);
            }
        }
    }

}
