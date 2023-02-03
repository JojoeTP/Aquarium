using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public string type;

    public DialogueInfo(string Id, string Character, string CharacterImage, string DialogueText, string Choice1, string Choice2, string Choice1Text, string Choice2Text, string Type)
    {
        ID = Id;
        character = Character;
        charaterImage = CharacterImage;
        dialogueText = DialogueText;
        choice1 = Choice1;
        choice2 = Choice2;
        choice1Text = Choice1Text;
        choice2Text = Choice2Text;
        type = Type;
    }
}