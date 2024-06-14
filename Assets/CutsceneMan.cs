using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CutsceneMan : MonoBehaviour
{
    public GameObject maindialogue;


    private Player player;
    public void Start()
    {
        player = new Player();
        player.main.Quack.performed += OnInteract;
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("GotDialogue");
        maindialogue.GetComponent<NPC>().Dialogue();
    }
}


