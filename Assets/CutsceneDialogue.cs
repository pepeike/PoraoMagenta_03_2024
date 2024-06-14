using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CutsceneDialogue : MonoBehaviour
{
    [SerializeField]
    private NPC npc;

    [SerializeField]
    private Player player;



    private void Awake()
    {
        player = new Player();
        npc.Invoke("Dialogue", 1f);
    }

    private void OnEnable()
    {
        player.main.Quack.Enable();
        player.main.Quack.performed += NextDialogue;
    }

    private void NextDialogue(InputAction.CallbackContext obj)
    {
        Debug.Log("lol");
        npc.Dialogue();
    }
}
