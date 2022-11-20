using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueInfo
{
    public string ID;
    public string character;
    public string charaterImage;
    public string dialogueText;
    public string[] choice;

    public DialogueInfo(string id, string characterName, string characterImageName, string dialogueTextName)
    {
        ID = id;
        character = characterName;
        charaterImage = characterImageName;
        dialogueText = dialogueTextName;
    }

}

public class DialogueManager_2 : MonoBehaviour
{
    public Dictionary<string, DialogueInfo> openWith = new Dictionary<string, DialogueInfo>();
    
    public static DialogueManager_2 inst;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    private Queue<string> sentences;

    Sprite character1;
    Sprite character2;
    ReadCSV_2 readCSVData;
    [SerializeField] int dialogueCount = 0;
    GameObject imageCharacter1;
    GameObject imageCharacter2;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] GameObject continueButton;
    //[SerializeField] GameObject decisionButton1;
    //[SerializeField] TextMeshProUGUI decisionButton1Text;
    //[SerializeField] GameObject decisionButton2;
    //[SerializeField] TextMeshProUGUI decisionButton2Text;

    string loadSpriteName;

    private void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        readCSVData = GetComponent<ReadCSV_2>();
        //DialogueInfo dialogueInfo = new DialogueInfo("1", "2", "3", "4");
        DialogueInfo dialogueInfo = new DialogueInfo("1", "2", "3", "4");
        Debug.Log(dialogueInfo.choice);
    }

    void CheckResourcesLoadSprite(string readCSVData_CharacterName)
    {
        if (readCSVData_CharacterName == "Amelia_disgusting")
        {
            loadSpriteName = "Amelia_disgusting";
        }
        else if (readCSVData_CharacterName == "Amelia_shocking")
        {
            loadSpriteName = "Amelia_shocking";
        }
        else if (readCSVData_CharacterName == "None")
        {
            loadSpriteName = "None";
        }

    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        nameText.text = readCSVData.characterName[0];
        dialogueText.text = readCSVData.dialogue[0];
        // character1 = Resources.Load<Sprite>("Dialogue/CharacterImage/" + readCSVData.character[0]);
        if (readCSVData.characterImage[0] == "None")
        {
            //None All
        }
        character1 = Resources.Load<Sprite>("Dialogue/CharacterImage/" + readCSVData.characterImage[0]);

        imageCharacter1 = GameObject.Find("Character1");
        imageCharacter1.GetComponent<Image>().sprite = character1;

        imageCharacter2 = GameObject.Find("Character2");
        imageCharacter2.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(readCSVData.dialogue[dialogueCount]));
        dialogueCount++;
    }

    public void DisplayNextSentence()
    {
        if (dialogueCount == readCSVData.dialogue.Count - 1)
        {
            EndDialogue();
            return;
        }
        dialogueCount++;

        if (readCSVData.characterName[0] != readCSVData.characterName[dialogueCount])
        {
            character2 = Resources.Load<Sprite>("Dialogue/CharacterImage/" + readCSVData.characterImage[dialogueCount]);

            imageCharacter2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            imageCharacter2.GetComponent<Image>().sprite = character2;
        }

        if (readCSVData.characterName[0] == readCSVData.characterImage[dialogueCount])
        {
            imageCharacter1.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            if (imageCharacter2.GetComponent<Image>().sprite != null)
            {
                imageCharacter2.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
        }
        else if (readCSVData.characterName[1] == readCSVData.characterImage[dialogueCount])
        {
            imageCharacter1.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            imageCharacter2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        //if (readCSVData.decision1[dialogueCount] != "1")
        //{
        //    continueButton.SetActive(false);
        //    decisionButton1.SetActive(true);
        //    decisionButton2.SetActive(true);
        //    decisionButton1Text.text = readCSVData.decision1[dialogueCount];
        //    decisionButton2Text.text = readCSVData.decision2[dialogueCount];
        //}
        //else
        //{
        //    continueButton.SetActive(true);
        //    decisionButton1.SetActive(false);
        //    decisionButton2.SetActive(false);
        //}
        nameText.text = readCSVData.characterImage[dialogueCount];
        StopAllCoroutines();
        StartCoroutine(TypeSentence(readCSVData.dialogue[dialogueCount]));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

    }

    void EndDialogue()
    {
        dialogueCount = 0;
        imageCharacter1.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        imageCharacter2.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        dialoguePanel.SetActive(false);
        Debug.Log("End Conversation");
    }

    //public void Decision1Button()
    //{
    //    readCSVData.id += 0.1f;
    //    readCSVData.checkID = 0;
    //    readCSVData.Invoke("Read", 0);
    //    // dialogueCount--;
    //    DisplayNextSentence();
    //}
    //public void Decision2Button()
    //{
    //    readCSVData.id += 0.2f;
    //    readCSVData.checkID = 0;
    //    readCSVData.Invoke("Read", 0);
    //    DisplayNextSentence();
    //}
}
