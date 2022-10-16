using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    private Queue<string> sentences;

    Sprite character1;
    Sprite character2;
    ReadCSV readCSVData;
    int dialogueCount = 0;


    void Start()
    {
        readCSVData = GetComponent<ReadCSV>();
    }

    public void StartDialogue (){
        nameText.text = readCSVData.character[0];
        dialogueText.text = readCSVData.dialogue[0];

        character1 = Resources.Load<Sprite>("Dialogue/CharacterImage/" + readCSVData.character[0]);
		GameObject imageCharacter1 = GameObject.Find ("Character1");
		imageCharacter1.GetComponent<Image>().sprite = character1;
    }

    public void DisplayNextSentence(){
        if(dialogueCount == readCSVData.dialogue.Count - 1){
            EndDialogue();
            return;
        }
        dialogueCount++;

        if(readCSVData.character[0] != readCSVData.character[dialogueCount]){
            character2 = Resources.Load<Sprite>("Dialogue/CharacterImage/" + readCSVData.character[dialogueCount]);

            GameObject imageCharacter2 = GameObject.Find ("Character2");
            imageCharacter2.GetComponent<Image>().sprite = character2;
        }

        nameText.text = readCSVData.character[dialogueCount];
        dialogueText.text = readCSVData.dialogue[dialogueCount];
    }

    void EndDialogue(){
        Debug.Log("End Conversation");
    }
}
