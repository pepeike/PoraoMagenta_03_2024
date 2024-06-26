using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class IntroCutscene : MonoBehaviour
{
    public string NPCName;
    [SerializeField] AssetLabelReference portraitsReference;

    [SerializeField]
    private NPCDialogue npcDialogue;



    //public List<DialogueLine> lines;
    public List<DialogueLine> dialogueSet;

    private GameObject marker;


    private void Awake()
    {
        //marker = GetComponentInChildren<TextMeshPro>().gameObject;
        //marker.SetActive(false);


    }





    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //marker.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //marker.SetActive(false);
        }
    }

    public void Dialogue()
    {
        Debug.Log(dialogueSet[0].speaker);
        if (npcDialogue.dialogueStarted == false)
        {
            npcDialogue.InitializeDialogue(dialogueSet);
        }
        else
        {
            npcDialogue.NextText();
            EndIntro();


        }


    }

    public void EndIntro()
    {
        if(npcDialogue.activeLine == null)
        {
            SceneManager.LoadScene("Level1");
        }
        
            

    }

}