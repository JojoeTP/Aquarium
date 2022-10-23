using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour

{
    public static DialogueManager inst;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    private Queue<string> sentences;

    Sprite character1;
    Sprite character2;
    ReadCSV readCSVData;
    int dialogueCount = 0;
    GameObject imageCharacter1;
    GameObject imageCharacter2;
    [SerializeField] GameObject dialoguePanel;

    private void Awake() {
        inst = this;
    }
    void Start()
    {
        readCSVData = GetComponent<ReadCSV>();
    }

    public void StartDialogue (){
        dialoguePanel.SetActive(true);
        nameText.text = readCSVData.character[0];
        dialogueText.text = readCSVData.dialogue[0];

        character1 = Resources.Load<Sprite>("Dialogue/CharacterImage/" + readCSVData.character[0]);
		imageCharacter1 = GameObject.Find ("Character1");
		imageCharacter1.GetComponent<Image>().sprite = character1;

        imageCharacter2 = GameObject.Find ("Character2");
        imageCharacter2.GetComponent<Image>().color = new Color(0,0,0,0);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(readCSVData.dialogue[dialogueCount]));
        dialogueCount++;
    }

    public void DisplayNextSentence(){
        if(dialogueCount == readCSVData.dialogue.Count - 1){
            EndDialogue();
            return;
        }
        dialogueCount++;

        if(readCSVData.character[0] != readCSVData.character[dialogueCount]){
            character2 = Resources.Load<Sprite>("Dialogue/CharacterImage/" + readCSVData.character[dialogueCount]);
            imageCharacter2.GetComponent<Image>().color = new Color(1,1,1,1);
            imageCharacter2.GetComponent<Image>().sprite = character2;
        }

        if(readCSVData.character[0] == readCSVData.character[dialogueCount]){
            imageCharacter1.GetComponent<Image>().color = new Color(1,1,1,1);
            if(imageCharacter2.GetComponent<Image>().sprite != null){
                imageCharacter2.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,1);
            }
        }
        else if(readCSVData.character[1] == readCSVData.character[dialogueCount]){
            imageCharacter1.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,1);
            imageCharacter2.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        nameText.text = readCSVData.character[dialogueCount];
        StopAllCoroutines();
        StartCoroutine(TypeSentence(readCSVData.dialogue[dialogueCount]));
    }

    IEnumerator TypeSentence(string sentence){
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()){
            dialogueText.text += letter;
            yield return null;
        }

    }

    void EndDialogue(){
        dialogueCount = 0;
        imageCharacter1.GetComponent<Image>().color = new Color(1,1,1,1);
        imageCharacter2.GetComponent<Image>().color = new Color(0,0,0,0);
        dialoguePanel.SetActive(false);
        Debug.Log("End Conversation");
    }
}
