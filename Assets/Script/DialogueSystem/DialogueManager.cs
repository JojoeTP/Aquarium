using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class DialogueInfo
{
    public string ID;
    public string character;
    public string charaterImage;
    public string dialogueText;
    public string choice1;
    public string choice2;
    public string choice1Text;
    public string choice2Text;

    public DialogueInfo(string Id, string Character, string CharacterImage, string DialogueText, string Choice1, string Choice2, string Choice1Text, string Choice2Text)
    {
        ID = Id;
        character = Character;
        charaterImage = CharacterImage;
        dialogueText = DialogueText;
        choice1 = Choice1;
        choice2 = Choice2;
        choice1Text = Choice1Text;
        choice2Text = Choice2Text;
    }
}

public class DialogueManager : MonoBehaviour
{
    [SerializeField] string[] dialoguePaths;
    public Dictionary<string, DialogueInfo> openWith = new Dictionary<string, DialogueInfo>();

    public static DialogueManager inst;

    [Header("DialogueText")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    [Header("CharacterImage")]
    Sprite character1;
    Sprite character2;
    [SerializeField] GameObject imageCharacter1;
    [SerializeField]  GameObject imageCharacter2;
    [SerializeField] GameObject dialoguePanel;

    [Header("Button")]
    [SerializeField] Button continueButton;
    [SerializeField] Button choiceButton1;
    [SerializeField] Button choiceButton2;

    [Header("ChoiceText")]
    [SerializeField] TextMeshProUGUI choiceButton1Text;
    [SerializeField] TextMeshProUGUI choiceButton2Text;

    [SerializeField] List<Sprite> characterSprites = new List<Sprite>();
    [SerializeField] string currentId;
    public TalkWithNPC currentNPC;
    public string currentDialogue;

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        dialoguePanel.SetActive(false);
        AddListenerToButton();
        LoadDialogueData();
    }
    void LoadDialogueData()
    {
        for (int i = 0; i < dialoguePaths.Length; i++)
        {
            StreamReader stringReader = new StreamReader(Application.streamingAssetsPath + "/DialogueData/" + dialoguePaths[i] + ".csv");

            bool endOfFile = false;
            while (!endOfFile)
            {
                string data_string = stringReader.ReadLine();

                if (data_string == null)
                {
                    endOfFile = true;
                    break;
                }

                var data_values = data_string.Split(',');


                if (data_values[0] == "Id" || data_values[0] == "")
                {

                }
                else
                {
                    //ID,character,charaterImage,dialogueText,choice1,choice2,choice1Text,choice2Text
                    DialogueInfo newDialogue = new DialogueInfo(data_values[0], data_values[1], data_values[2], data_values[3], data_values[4], data_values[5], data_values[6], data_values[7]);
                    //Debug.Log(newDialogue.character);
                    openWith.Add(data_values[0], newDialogue);
                }
            }
        }
    }

    void CheckMainCharacterSpeak(string dialogueId)
    {
        // if (openWith[dialogueId].character == "อเมเลีย")
        if (openWith[dialogueId].charaterImage.Contains("Amelia"))
        {
            //character1 = Resources.Load<Sprite>("Dialogue/CharacterImage/" + openWith[dialogueId].charaterImage);
            character1 = characterSprites.Find(n => n.name == openWith[dialogueId].charaterImage);
            imageCharacter1.GetComponent<Image>().sprite = character1;
            imageCharacter1.GetComponent<Image>().color = new Color(1,1,1,1);
            if(character2 != null)
            {
                imageCharacter2.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                imageCharacter2.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
        }
        else
        {
            //character2 = Resources.Load<Sprite>("Dialogue/CharacterImage/" + openWith[dialogueId].charaterImage);
            character2 = characterSprites.Find(n => n.name == openWith[dialogueId].charaterImage);
            if (character1 != null)
            {
                imageCharacter1.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                imageCharacter1.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
            imageCharacter2.GetComponent<Image>().sprite = character2;
            imageCharacter2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        if (openWith[dialogueId].charaterImage == "None")
        {
            if(imageCharacter1.GetComponent<Image>().sprite != null)
            {
                imageCharacter1.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                imageCharacter1.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }

            if (imageCharacter2.GetComponent<Image>().sprite != null)
            {
                imageCharacter2.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                imageCharacter2.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
        }
    }

    public void StartDialogue()
    {
        if (currentDialogue == null)
        {
            return;
        }
        dialoguePanel.SetActive(true);
        CheckIfHaveChoice(currentDialogue);

        CheckMainCharacterSpeak(currentDialogue);
        nameText.text = openWith[currentDialogue].character;
        dialogueText.text = openWith[currentDialogue].dialogueText;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(openWith[currentDialogue].dialogueText));
        if (isChoice == false)
        {
            currentId = openWith[currentDialogue].choice1;
        }
        else
        {
            currentId = currentDialogue;
        }
    }

    bool isChoice = false;

    void CheckIfHaveChoice(string checkChoiceId)
    {
        if (openWith[checkChoiceId].choice1 == "" && openWith[checkChoiceId].choice2 == "" || openWith[checkChoiceId].choice1 != "" && openWith[checkChoiceId].choice2 == "")
        {
            //end dialogue or continue
            isChoice = false;
            continueButton.gameObject.SetActive(true);
            ToggleChoiceButton(false);
            //currentId = openWith[checkChoiceId].choice1;
        }
        else if (openWith[checkChoiceId].choice1 != "" && openWith[checkChoiceId].choice2 != "")
        {
            // choice
            isChoice = true;
            continueButton.gameObject.SetActive(false);
            ToggleChoiceButton(true);
            choiceButton1Text.text = openWith[checkChoiceId].choice1Text;
            choiceButton2Text.text = openWith[checkChoiceId].choice2Text;
        }
    }

    void ToggleChoiceButton(bool enabled)
    {
        choiceButton1.gameObject.SetActive(enabled);
        choiceButton2.gameObject.SetActive(enabled);
    }

    public void DecisionButton1()
    {
        currentId = openWith[currentId].choice1;
        DisplayNextSentence();
    }

    public void DecisionButton2()
    {
        currentId = openWith[currentId].choice2;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (currentId == "")
        {
            EndDialogue();
            return;
        }
        CheckIfHaveChoice(currentId);

        CheckMainCharacterSpeak(currentId);
        
        nameText.text = openWith[currentId].character;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(openWith[currentId].dialogueText));
        if(isChoice == false)
        {
            currentId = openWith[currentId].choice1;
        }
    }

    void AddListenerToButton()
    {
        continueButton.onClick.RemoveAllListeners();
        choiceButton1.onClick.RemoveAllListeners();
        choiceButton2.onClick.RemoveAllListeners();

        continueButton.onClick.AddListener(() => {DisplayNextSentence();});
        choiceButton1.onClick.AddListener(() => {DecisionButton1();});
        choiceButton2.onClick.AddListener(() => {DecisionButton2();});
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
        isChoice = false;
        dialoguePanel.SetActive(false);
        ResetCharacterSprite();

        if(currentNPC != null)
        {
            currentNPC.triggerEvents.Invoke();
            currentNPC = null;
        }
        currentDialogue = null;
        PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;
        Debug.Log("End Conversation");
    }

    void ResetCharacterSprite()
    {
        character1 = null;
        character2 = null;
        imageCharacter1.GetComponent<Image>().sprite = null;
        imageCharacter1.GetComponent<Image>().sprite = null;
        imageCharacter1.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        imageCharacter2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

}
