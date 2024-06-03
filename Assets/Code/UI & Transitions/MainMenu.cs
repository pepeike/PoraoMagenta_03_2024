using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public Animator screenwipe;
    public void StartingGame()
    {
        screenwipe.SetTrigger("Wipe");
    }
}
