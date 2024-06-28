using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public Animator screenwipe;
    public GameObject creditstab;
    public void StartingGame()
    {
        screenwipe.SetTrigger("Wipe");
    }

    public void Credits()
    {
        creditstab.SetActive(true);
    }

    public void RemoveCredits()
    {
        creditstab.SetActive(false);
    }
        
}
