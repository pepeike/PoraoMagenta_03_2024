using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLife : MonoBehaviour
{

    [SerializeField]
    private int life;
    [SerializeField]
    private SwanBossBehavior swanBoss;
    [SerializeField]
    private RatBossBehavior ratBoss;

    public event EventHandler OnZeroLife;
    
    private bool isInvulnerable;

    private void Update() {
        if (swanBoss != null) {
            isInvulnerable = swanBoss.isInvulnerable;
        } else {
            isInvulnerable = ratBoss.isInvulnerable;
        }
    }

    public void TakeDamage(int damage) {
        if (!isInvulnerable)
        {
            if (life > 1) {
                life -= damage;
            } else {
                OnZeroLife?.Invoke(this, EventArgs.Empty);
                //Destroy(gameObject);
            }
        }
    }

}
