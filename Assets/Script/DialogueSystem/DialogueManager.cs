using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject UIDialoguePanel;
    [SerializeField] string[] dialoguePaths;
    public Dictionary<string, DialogueInfo> openWith = new Dictionary<string, DialogueInfo>();

    public static DialogueManager inst;
    public enum Type
    {
        Dialogue,
        CutScene
    }
    [Header("DialogueType")]
    public Type type;
    Type beforeCurrentType;

    
    bool isChoice = false;

    //[SerializeField] Sprite[] loadSprite;
    [SerializeField] List<Sprite> characterSprites = new List<Sprite>();
    [SerializeField] string currentId;
    public TalkWithNPC currentNPC;
    public string currentDialogue;

    Canvas dialogueCanvas;
    public UIDialoguePanel dialoguePanel;

    void LoadCharacterSprites()
    {
        //loadSprite = (Sprite[])Resources.LoadAll("CutScene");
        characterSprites = Resources.LoadAll<Sprite>("DialogueSprite").ToList();
    }
    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        LoadAllDialogueData();
        LoadCharacterSprites();
    }

    void Initialize()
    {
        CrateNewDialoguePanel();

        dialogueCanvas.enabled = false;

        dialoguePanel.ResetCharacterSprite();
        dialoguePanel.AddListenerToButton(DisplayNextSentence,DecisionButton1,DecisionButton2);
    }

    void CrateNewDialoguePanel()
    {
        var _dialoguePanel = Instantiate(UIDialoguePanel);
        dialoguePanel = _dialoguePanel.GetComponent<UIDialoguePanel>();
        dialogueCanvas = dialoguePanel.GetComponent<Canvas>();
    }

    async void LoadAllDialogueData()
    {
        for (int i = 0; i < dialoguePaths.Length; i++)
        {
            await LoadDialogueData(i);
        }
    }

    public async Task LoadDialogueData(int index)
    {
        StreamReader stringReader = new StreamReader(Application.streamingAssetsPath + "/DialogueData/" + dialoguePaths[index] + ".csv");

        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_string = stringReader.ReadLine();

            if (data_string == null)
            {
                endOfFile = true;
                break;
            }

            var data_values = data_string.Split(';');


            if (data_values[0] == "Id" || data_values[0] == "")
            {

            }
            else
            {
                //ID,character,charaterImage,dialogueText,choice1,choice2,choice1Text,choice2Text,type
                Debug.Log("Loading Dialogue " + data_values[0]);
                DialogueInfo newDialogue = new DialogueInfo(data_values[0], data_values[1], data_values[2], data_values[3], data_values[4], data_values[5], data_values[6], data_values[7], data_values[8]);
                openWith.Add(data_values[0], newDialogue);
                await Task.Yield();
                //print(data_values[0]);
            }
        }
        
    }

    void CheckMainCharacterSpeak(string dialogueId)
    {
        if (openWith[dialogueId].charaterImage.Contains("Amelia"))
        {
            //character1 = Resources.Load<Sprite>("Dialogue/CharacterImage/" + openWith[dialogueId].charaterImage);
            var character1 = characterSprites.Find(n => n.name == openWith[dialogueId].charaterImage);
            dialoguePanel.SetCharacter1Sprite(character1);
            dialoguePanel.ImageCharacter1.GetComponent<Image>().color = new Color(1,1,1,1);
            if(dialoguePanel.Character2Sprite != null)
            {
                dialoguePanel.ImageCharacter2.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                dialoguePanel.ImageCharacter2.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
        }
        else
        {
            //character2 = Resources.Load<Sprite>("Dialogue/CharacterImage/" + openWith[dialogueId].charaterImage);
            var character2 = characterSprites.Find(n => n.name == openWith[dialogueId].charaterImage);
            if (dialoguePanel.Character1Sprite != null)
            {
                dialoguePanel.ImageCharacter1.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                dialoguePanel.ImageCharacter1.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
            dialoguePanel.SetCharacter2Sprite(character2);
            dialoguePanel.ImageCharacter2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        if (openWith[dialogueId].charaterImage == "None")
        {
            if(dialoguePanel.ImageCharacter1.GetComponent<Image>().sprite != null)
            {
                dialoguePanel.ImageCharacter1.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                dialoguePanel.ImageCharacter1.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }

            if (dialoguePanel.ImageCharacter2.GetComponent<Image>().sprite != null)
            {
                dialoguePanel.ImageCharacter2.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                dialoguePanel.ImageCharacter2.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
        }
    }

    public void StartDialogue()
    {
        if(dialoguePanel == null)
            Initialize();

        if (currentDialogue == null)
        {
            return;
        }
        type = (Type)Enum.Parse(typeof(Type), openWith[currentDialogue].type);
        dialogueCanvas.enabled = true;
        CheckIfHaveChoice(currentDialogue);
        CheckIfHaveDialogueBg(currentNPC);
        CheckDialogueType(currentDialogue , false);

        dialoguePanel.NameText.text = openWith[currentDialogue].character;
        dialoguePanel.DialogueText.text = openWith[currentDialogue].dialogueText;
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

    void CheckIfHaveDialogueBg(TalkWithNPC currentNPC)
    {
        if (currentNPC.haveDialogueBg == true)
        {
            dialoguePanel.DialogueBg.SetActive(true);
        }
    }
    void CheckDialogueType(string checkChoiceId , bool checkTransition)
    {
        if (type != beforeCurrentType && checkTransition)
        {
            UITransition.inst.CutSceneTransitionIn();
            if (type == Type.Dialogue)
            {
                dialoguePanel.ResetCharacterSprite();
            }
        }
        if (type == Type.CutScene)
        {
            var cutSceneImage = characterSprites.Find(n => n.name == openWith[checkChoiceId].charaterImage);
            dialoguePanel.SetCutSceneSprite(cutSceneImage);
            dialoguePanel.ImageCutScene.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else if (type == Type.Dialogue)
        {
            CheckMainCharacterSpeak(checkChoiceId);
        }
        beforeCurrentType = type;
    }
    

    void CheckIfHaveChoice(string checkChoiceId)
    {
        if (openWith[checkChoiceId].choice1 == "" && openWith[checkChoiceId].choice2 == "" || openWith[checkChoiceId].choice1 != "" && openWith[checkChoiceId].choice2 == "")
        {
            //end dialogue or continue
            isChoice = false;
            dialoguePanel.ContinueButton.gameObject.SetActive(true);
            ToggleChoiceButton(false);
            //currentId = openWith[checkChoiceId].choice1;
        }
        else if (openWith[checkChoiceId].choice1 != "" && openWith[checkChoiceId].choice2 != "")
        {
            // choice
            isChoice = true;
            dialoguePanel.ContinueButton.gameObject.SetActive(false);
            ToggleChoiceButton(true);
            dialoguePanel.ChoiceButton1Text.text = openWith[checkChoiceId].choice1Text;
            dialoguePanel.ChoiceButton2Text.text = openWith[checkChoiceId].choice2Text;
        }
    }

    void ToggleChoiceButton(bool enabled)
    {
        dialoguePanel.ChoiceButton1.gameObject.SetActive(enabled);
        dialoguePanel.ChoiceButton2.gameObject.SetActive(enabled);
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
            if (type == Type.CutScene)
            {
                UITransition.inst.CutSceneTransitionIn();
            }
            EndDialogue();
            return;
        }
        type = (Type)Enum.Parse(typeof(Type), openWith[currentId].type);
        CheckIfHaveChoice(currentId);
        CheckDialogueType(currentId , true);

        dialoguePanel.NameText.text = openWith[currentId].character;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(openWith[currentId].dialogueText));
        if (isChoice == false)
        {
            currentId = openWith[currentId].choice1;
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialoguePanel.DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialoguePanel.DialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        isChoice = false;
        dialogueCanvas.enabled = false;
        dialoguePanel.ResetCharacterSprite();

        if (currentNPC != null && currentNPC.isSave == true)
        {
            print("save");
            SaveGameSystemManager.inst.SaveGame();
        }
        print(currentNPC);
        if (currentNPC != null)
        {
            print(currentNPC.triggerEvents);
            currentNPC.triggerEvents.Invoke();
            currentNPC = null;
        }
        currentDialogue = null;
        PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;
    }
}
