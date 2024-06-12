using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour {
    private NPC activeNPC;


    private List<DialogueLine> NPCLines;
    private DialogueLine activeLine;
    private string NPCNameString;
    private int dialogueIndex;

    public GameObject dialogueWindow;

    public Image npcPortrait;
    public TMP_Text NPCName;
    public TMP_Text dialogueText;
    public TMP_Text endMarker;

    [SerializeField]
    private AssetLabelReference labelReference;
    [SerializeField]
    private List<Sprite> spr = new List<Sprite>();
    [SerializeField]
    private Dictionary<string, Sprite> portraitDictionary = new Dictionary<string, Sprite>();

    AsyncOperationHandle<IList<Sprite>> loadHandle;

    public bool readyToSpeak;
    public bool dialogueStarted;
    [SerializeField]
    private float letterTimer;





    private void Awake() {
        NPCLines = new List<DialogueLine>();
        dialogueWindow.SetActive(false);
        dialogueIndex = 0;
        readyToSpeak = true;
        dialogueStarted = false;

        LoadPortraits();

        
        
        DontDestroyOnLoad(transform);
    }

    

    private void LoadPortraits() {
        //List<Sprite> _port = new List<Sprite>();
        loadHandle = Addressables.LoadAssetsAsync<Sprite>(labelReference,
            addressable => {
                //int i = 0;
                portraitDictionary.Add(addressable.name, addressable);
            }
        );
        
        //int i = 0;
        //foreach (Sprite sprite in spr) {
            
        //    portraitDictionary.Add( "portrait" + i, sprite);
            
        //    i++;
        //}
    }

    public void InitializeDialogue(List<DialogueLine> dialogueSet) {

        foreach (DialogueLine dialogueLine in dialogueSet) {
            if (dialogueLine.canShow) {

                NPCLines.Add(dialogueLine);

            }
        }

        readyToSpeak = false;
        dialogueStarted = true;

        dialogueIndex = 0;

        if (portraitDictionary[NPCLines[dialogueIndex].portrait.ToString()] != null) {
            Debug.Log(NPCLines[dialogueIndex].portrait.ToString());
            npcPortrait.overrideSprite = portraitDictionary[NPCLines[dialogueIndex].portrait.ToString()];
        } else {
            npcPortrait.overrideSprite = null;
            Debug.Log("Couldn't load portrait. Enum value: " + NPCLines[dialogueIndex].portrait.ToString());
        }

            activeLine = NPCLines[dialogueIndex];
        NPCName.text = activeLine.speaker;
        dialogueWindow.SetActive(true);
        
        StartCoroutine(ShowText());



    }

    public void NextText() {
        if (readyToSpeak) {
            dialogueIndex++;
            endMarker.gameObject.SetActive(false);

            if (dialogueIndex > NPCLines.Count - 1) {
                EndDialogue();
            } else {
                activeLine = NPCLines[dialogueIndex];
                NPCName.text = activeLine.speaker;
                StartCoroutine(ShowText());
            }
        } else {
            StopAllCoroutines();
            dialogueText.text = activeLine.speakerLines;
            endMarker.gameObject.SetActive(true);
            readyToSpeak = true;
        }
        


    }

    IEnumerator ShowText() {
        dialogueText.text = "";
        readyToSpeak = false;


        foreach (char c in activeLine.speakerLines) {
            dialogueText.text += c;
            yield return new WaitForSeconds(letterTimer);
        }
        endMarker.gameObject.SetActive(true);
        readyToSpeak = true;

    }

    public void EndDialogue() {
        dialogueWindow.SetActive(false);
        dialogueStarted = false;
        readyToSpeak = true;
        dialogueIndex = 0;
        activeLine = null;
        NPCLines.Clear();
    }

}
