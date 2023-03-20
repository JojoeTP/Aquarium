using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.Events;


public class UIDialoguePanel : MonoBehaviour
{
    [Header("CharacterImage")]
    Sprite character1Sprite;
    Sprite character2Sprite;
    Sprite cutSceneSprite;
    
    [Header("DialogueText")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    [Header("CharacterImage")]
    [SerializeField] GameObject dialogueBg;
    [SerializeField] GameObject imageCharacter1;
    [SerializeField]  GameObject imageCharacter2;
    [SerializeField]  GameObject imageCutScene;

    [Header("Button")]
    [SerializeField] Button continueButton;
    [SerializeField] GameObject choiceButton1;
    [SerializeField] GameObject choiceButton2;


    public TextMeshProUGUI NameText {get {return nameText;}}
    public TextMeshProUGUI DialogueText {get {return dialogueText;}}
    public GameObject DialogueBg { get {return dialogueBg; }}
    public GameObject ImageCharacter1 {get {return imageCharacter1;}}
    public GameObject ImageCharacter2 {get {return imageCharacter2;}}
    public GameObject ImageCutScene {get {return imageCutScene;}}
    public Button ContinueButton {get {return continueButton;}}
    public Sprite Character1Sprite {get {return character1Sprite;}}
    public Sprite Character2Sprite {get {return character2Sprite;}}

    [HideInInspector]
    public Button ChoiceButton1;
    [HideInInspector]
    public Button ChoiceButton2;
    [HideInInspector]
    public TMP_Text ChoiceButton1Text;
    [HideInInspector]
    public TMP_Text ChoiceButton2Text;

    void GetAllComponent()
    {
        ChoiceButton1 = choiceButton1.GetComponent<Button>();
        ChoiceButton2 = choiceButton2.GetComponent<Button>();
        ChoiceButton1Text = choiceButton1.GetComponentInChildren<TMP_Text>();
        ChoiceButton2Text = choiceButton2.GetComponentInChildren<TMP_Text>();
    }

    public void AddListenerToButton(UnityAction displayNextSentence,UnityAction decisionButton1,UnityAction decisionButton2)
    {
        GetAllComponent();

        ContinueButton.onClick.RemoveAllListeners();
        ChoiceButton1.onClick.RemoveAllListeners();
        ChoiceButton2.onClick.RemoveAllListeners();

        ContinueButton.onClick.AddListener(displayNextSentence);
        ChoiceButton1.onClick.AddListener(decisionButton1);
        ChoiceButton2.onClick.AddListener(decisionButton2);
    }

    public void ResetCharacterSprite()
    {
        character1Sprite = null;
        character2Sprite = null;
        imageCharacter1.GetComponent<Image>().sprite = null;
        imageCharacter2.GetComponent<Image>().sprite = null;
        imageCharacter1.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        imageCharacter2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        imageCutScene.GetComponent<Image>().sprite = null;
        imageCutScene.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        dialogueBg.SetActive(false);
    }

    public void SetCharacter1Sprite(Sprite character)
    {
        character1Sprite = character;
        imageCharacter1.GetComponent<Image>().sprite = character1Sprite;
    }

    public void SetCharacter2Sprite(Sprite character)
    {
        character2Sprite = character;
        imageCharacter2.GetComponent<Image>().sprite = character2Sprite;
    }

    public void SetCutSceneSprite(Sprite image)
    {
        cutSceneSprite = image;
        imageCutScene.GetComponent<Image>().sprite = cutSceneSprite;
    }
}
