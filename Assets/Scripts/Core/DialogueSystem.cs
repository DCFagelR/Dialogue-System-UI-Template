using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{

// ++Variables+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public const string TRANS_TAG = "<color=#00000000>";
    public const string COLOR_TAG_END = "</color>";

    // static instance so it can be located by instance rather than having a search procedure
    public static DialogueSystem instance;
    public string tempDialogue = "";
    [HideInInspector] public bool isWaitingForUser = false;
    Coroutine talking;

    public ELEMENTS elements;
    [System.Serializable]
    public class ELEMENTS {
        public GameObject dialoguePanel;
        public Text nameText;
        public Text dialogueText;
    }

// ++Functions+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    void Awake() {
        instance = this; // ensures only 1 in the scene
    }

// ----------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start() {
    }

// ----------------------------------------------------------------------------
    
    // Update is called once per frame
    void Update() {
    }

// ----------------------------------------------------------------------------

    // Get elements; for easier access
    public GameObject dialoguePanel {get{return elements.dialoguePanel;}}
    public Text nameText {get{return elements.nameText;}}
    public Text dialogueText {get{return elements.dialogueText;}}

// ----------------------------------------------------------------------------

    // Shows speech in the text box while someone is currently speaking
    // input: dialogue -> text you want to display in the textbox
    //       name -> name of the current speaker
    public void Say(string dialogue, string name) {
        StopTalking();
        talking = StartCoroutine(TalkingRoutine(dialogue, name));
    }

// ----------------------------------------------------------------------------

    public void StopTalking() {
        if(isTalking) {
            removeColorTags();
            dialogueText.text = tempDialogue;
            StopCoroutine(talking);
        }
        talking = null;
    }

// ----------------------------------------------------------------------------

    public bool isTalking {
        get{return talking != null;}
    }

// ----------------------------------------------------------------------------

    public void removeColorTags() {
        tempDialogue = tempDialogue.Replace(TRANS_TAG, "");
        tempDialogue = tempDialogue.Replace(COLOR_TAG_END, "");
    }

// ----------------------------------------------------------------------------

    // coroutine for Say(). While running = someone is currently speaking
    IEnumerator TalkingRoutine(string dialogue, string name = "") {
        int index = 1;

        isWaitingForUser = false;

        dialoguePanel.SetActive(true); // Move this eventually?
        nameText.text = name;

        // Initial text is all invisible on creation
        tempDialogue = "<color=#00000000>" + dialogue + "</color>";
        dialogueText.text = ""; // Empty last entered dialogue

        while(index < dialogue.Length + 1) {
            // Iterate the transparent tag through the text 1 char at a time
            // This fixes the word wrap being janky when writing 1 char at a time
            tempDialogue = tempDialogue.Remove(index-1, 17);
            tempDialogue = tempDialogue.Insert(index, "<color=#00000000>");
            dialogueText.text = tempDialogue;

            index++;
            yield return new WaitForEndOfFrame();
        }

        // remove color tags at the end
        removeColorTags();
        dialogueText.text = tempDialogue;

        isWaitingForUser = true;
        while(isWaitingForUser) {
            yield return new WaitForEndOfFrame();
        }

        StopTalking();
    }
    
// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++    

}
