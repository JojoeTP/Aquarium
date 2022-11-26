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
    public Dictionary<string, DialogueInfo> openWith = new Dictionary<string, DialogueInfo>();

    public static DialogueManager inst;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    Sprite character1;
    Sprite character2;
    [SerializeField] GameObject imageCharacter1;
    [SerializeField]  GameObject imageCharacter2;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] GameObject continueButton;

    [SerializeField] GameObject choiceButton1;
    [SerializeField] TextMeshProUGUI choiceButton1Text;
    [SerializeField] GameObject choiceButton2;
    [SerializeField] TextMeshProUGUI choiceButton2Text;

    private void Awake()
    {
        inst = this;
    }
    [SerializeField] string currentId;
    void CheckMainCharacterSpeak(string dialogueId)
    {
        if (openWith[dialogueId].character == "อเมเลีย")
        {
            character1 = Resources.Load<Sprite>("Dialogue/CharacterImage/" + openWith[dialogueId].charaterImage);
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
            character2 = Resources.Load<Sprite>("Dialogue/CharacterImage/" + openWith[dialogueId].charaterImage);
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
            imageCharacter1.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            imageCharacter2.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }
    public void StartDialogue(string startWithDialogueId)
    {
        dialoguePanel.SetActive(true);
        CheckIfHaveChoice(startWithDialogueId);

        CheckMainCharacterSpeak(startWithDialogueId);
        nameText.text = openWith[startWithDialogueId].character;
        dialogueText.text = openWith[startWithDialogueId].dialogueText;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(openWith[startWithDialogueId].dialogueText));
        if (isChoice == false)
        {
            currentId = openWith[startWithDialogueId].choice1;
        }
    }
    bool isChoice = false;

    void CheckIfHaveChoice(string checkChoiceId)
    {
        if (openWith[checkChoiceId].choice1 == "" && openWith[checkChoiceId].choice2 == "" || openWith[checkChoiceId].choice1 != "" && openWith[checkChoiceId].choice2 == "")
        {
            //end dialogue or continue
            isChoice = false;
            continueButton.SetActive(true);
            choiceButton1.SetActive(false);
            choiceButton2.SetActive(false);
            //currentId = openWith[checkChoiceId].choice1;
        }
        else if (openWith[checkChoiceId].choice1 != "" && openWith[checkChoiceId].choice2 != "")
        {
            // choice
            isChoice = true;
            continueButton.SetActive(false);
            choiceButton1.SetActive(true);
            choiceButton2.SetActive(true);
            choiceButton1Text.text = openWith[checkChoiceId].choice1Text;
            choiceButton2Text.text = openWith[checkChoiceId].choice2Text;
        }
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
        Debug.Log("End Conversation");
    }

    void ResetCharacterSprite()
    {
        character1 = null;
        character2 = null;
        imageCharacter1.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        imageCharacter2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

}
