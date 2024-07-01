using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour
{

    public int healthcount;
    public PlayerControl playerControl;
    public Image Heart1;
    public Image Heart2;
    public Image Heart3;

    

    public void Awake() {
        healthcount = playerControl.hitPoints;
        playerControl.tookDmg += TakeDamage;
    }

    public void TakeDamage(object sender, System.EventArgs e)
    {
        healthcount -= 1;
        
        switch (healthcount)
        {
            case 2:
                Heart3.enabled = false;
                break;

            case 1:
                Heart2.enabled = false;
                break;

            case 0:
                Heart1.enabled = false;
                break;

        }

    }

    public void GainHealth()
    {
        healthcount += 1;

        switch (healthcount)
        {
            case 3:
                Heart3.enabled = true;
                break;

            case 2:
                Heart2.enabled = true;
                break;

            case 1:
                Heart1.enabled = true;
                break;
        }
    }
}
