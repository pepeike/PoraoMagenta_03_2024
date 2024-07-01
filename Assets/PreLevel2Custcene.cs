using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class PreLevel2Custcene : MonoBehaviour
{
    public string NPCName;
    [SerializeField] AssetLabelReference portraitsReference;

    [SerializeField]
    private NPCDialogue npcDialogue;
    private int currentScene;


    //public List<DialogueLine> lines;
    public List<DialogueLine> dialogueSet;

    private GameObject marker;


    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
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
            EndCutscene1();


        }


    }

    public void EndCutscene1()
    {
        if (npcDialogue.activeLine == null)
        {
            SceneManager.UnloadSceneAsync(currentScene);
            SceneManager.LoadSceneAsync(currentScene + 1);
            
        }



    }

}
