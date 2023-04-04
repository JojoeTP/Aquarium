using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine.Events;

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
    bool dialogueHasChoice = false;

    //[SerializeField] Sprite[] loadSprite;
    [SerializeField] List<Sprite> characterSprites = new List<Sprite>();
    [SerializeField] string currentId;
    public TalkWithNPC currentNPC;
    public string currentDialogue;

    Canvas dialogueCanvas;
    public UIDialoguePanel dialoguePanel;

    [SerializeField] float typeDialogueDelay;
    [SerializeField] int dialogueSpeedMultiplyer;

    bool loadingDialogueData;

    public void LoadCharacterSprites()
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
        StartCoroutine(LoadAllDialogueData());
        LoadCharacterSprites();
    }

    public bool IsLoadingDialogueData()
    {
        return loadingDialogueData;
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

    public IEnumerator LoadAllDialogueData()
    {
        for (int i = 0; i < dialoguePaths.Length; i++)
        {
            LoadDialogueData(i);
            yield return null;
        }
        
        loadingDialogueData = true;
    }

    public void LoadDialogueData(int index)
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

            var data_values = data_string.Split(',');


            if (data_values[0] == "Id" || data_values[0] == "")
            {

            }
            else
            {
                //ID,character,charaterImage,dialogueText,choice1,choice2,choice1Text,choice2Text,type,sound,BGMSound
                Debug.Log("Loading Dialogue " + data_values[0] + "" + data_values[8]);
                //Todo : แก้ตรงนี้ type หายในบางครั้ง
                DialogueInfo newDialogue = new DialogueInfo(data_values[0], data_values[1], data_values[2], data_values[3], data_values[4], data_values[5], data_values[6], data_values[7], data_values[8], data_values[9], data_values[10]);
                openWith.Add(data_values[0], newDialogue);
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
        SoundManager.Inst.MuteBGM();
        PlaySound(openWith[currentDialogue].sound);
        PlayBGMSound(openWith[currentDialogue].BGMSound);
        
        type = (Type)Enum.Parse(typeof(Type), openWith[currentDialogue].type);
        dialogueCanvas.enabled = true;
        CheckIfHaveChoice(currentDialogue);
        
        if(currentNPC != null)
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
    void PlaySound(string soundId)
    {
        if (soundId != "")
        {
            // SoundManager.Inst.PlayOneShot(FMODEvent.inst.FModEventDictionary[soundId], PlayerManager.inst.transform.position);
            SoundManager.Inst.InitializeDialogueSound(FMODEvent.inst.FModEventDictionary[soundId]);
        }
    }

    void StopSound()
    {
        SoundManager.Inst.StopDialogue();
    }

    void PlayBGMSound(string BGMSoundId)
    {
        if (BGMSoundId != "")
        {
            SoundManager.Inst.InitializeBGM(FMODEvent.inst.FModEventDictionary[BGMSoundId]);
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
        if (type != beforeCurrentType && checkTransition == true)
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
            
            if(!dialogueHasChoice)
                dialogueHasChoice = false;

            dialoguePanel.ContinueButton.gameObject.SetActive(true);
            ToggleChoiceButton(false);
            //currentId = openWith[checkChoiceId].choice1;
        }
        else if (openWith[checkChoiceId].choice1 != "" && openWith[checkChoiceId].choice2 != "")
        {
            // choice
            isChoice = true;
            dialogueHasChoice = true;
            dialoguePanel.ContinueButton.gameObject.SetActive(false);
            ToggleChoiceButton(true);
            dialoguePanel.ChoiceButton1Text.text = openWith[checkChoiceId].choice1Text;
            dialoguePanel.ChoiceButton2Text.text = openWith[checkChoiceId].choice2Text;
        }
    }

    void ToggleChoiceButton(bool enabled)
    {
        UnityAction active = () => currentNPC.triggerEvents.Invoke();
        dialoguePanel.ChoiceButton1.onClick.AddListener(active);
        dialoguePanel.ChoiceButton1.onClick.AddListener(() => dialoguePanel.ChoiceButton1.onClick.RemoveListener(active)); //ไม่แน่ใจว่าจะใช้ได้ไหม
        
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
        StopSound();
        if (currentId == "")
        {
            if (type == Type.CutScene)
            {
                UITransition.inst.CutSceneTransitionIn();
            }
            EndDialogue();
            return;
        }

        if(currentId == "End")
        {
            if (currentNPC != null)
            {
                if(!dialogueHasChoice)
                    currentNPC.triggerEvents.Invoke();
                
                currentNPC = null;
            }

            dialogueHasChoice = false;
            currentDialogue = null;
            PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;
            return;
        }

        PlaySound(openWith[currentId].sound);
        PlayBGMSound(openWith[currentId].BGMSound);
        if(openWith[currentId].type != "")
            type = (Type)Enum.Parse(typeof(Type), openWith[currentId].type);

        CheckIfHaveChoice(currentId);
        CheckDialogueType(currentId , true);

        dialoguePanel.NameText.text = openWith[currentId].character;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(openWith[currentId].dialogueText));
        if (isChoice == false)
        {
            if(currentId == "Ch4_D07_36")
                currentId = "End";
            else
                currentId = openWith[currentId].choice1;
        }
    }


    string typeSentence;
    IEnumerator TypeSentence(string sentence)
    {
        dialoguePanel.DialogueText.text = "";

        char[] count = sentence.ToCharArray();
        for (int i = 0; i < count.Length; i++)
        {
            typeSentence += count[i];
        }
        dialoguePanel.DialogueText.text += typeSentence;

        foreach (char letter in sentence.ToCharArray())
        {
            
            dialoguePanel.DialogueText.text += letter;
            //yield return new WaitForSeconds(typeDialogueDelay * dialogueSpeedMultiplyer);
            yield return null;
        }
        //foreach (char letter in sentence.ToCharArray())
        //{
        //    dialoguePanel.DialogueText.text += letter;
        //    //yield return new WaitForSeconds(typeDialogueDelay * dialogueSpeedMultiplyer);
        //    yield return null;
        //}
    }

    void EndDialogue()
    {
        isChoice = false;
        dialogueCanvas.enabled = false;
        dialoguePanel.ResetCharacterSprite();
        SoundManager.Inst.ContinuePlayBGM();
        StopSound();
        if (currentNPC != null && currentNPC.isSave == true)
        {
            print("save");
            SaveText.inst.ShowSaveText();
            SaveGameSystemManager.inst.SaveGame();
        }

        if (currentNPC != null)
        {
            if(!dialogueHasChoice)
                currentNPC.triggerEvents.Invoke();
            
            currentNPC = null;
        }

        dialogueHasChoice = false;
        currentDialogue = null;
        PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;
    }
}
