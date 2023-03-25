using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTransitionScript : MonoBehaviour
{
    Animator animator;
    TalkWithNPC talkWithNPC;
    public static CutSceneTransitionScript inst;

    private void Awake()
    {
        inst = this;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CutSceneTransitionIn()
    {
        animator.SetTrigger("CutSceneTransitionIn");
    }

    public void CutSceneTransitionOut()
    {
        animator.SetTrigger("CutSceneTransitionOut");
        if (talkWithNPC == null && getData == true)
        {
            PlayerManager.inst.PlayerInteract.StartDialogue(talkWithNPC);
            getData = false;
        }
        //PlayerManager.inst.PlayerInteract.Interacting();
    }
    bool getData;
    public void GetTalkWithNPC(TalkWithNPC talkWithNPCData , bool getBool)
    {
        talkWithNPC = talkWithNPCData;
        getData = getBool;
    }

    public void CloseContinueButtonInteract()
    {
        DialogueManager.inst.dialoguePanel.ContinueButton.interactable = false;
    }

    public void OpenContinueButtonInteract()
    {
        DialogueManager.inst.dialoguePanel.ContinueButton.interactable = true;
    }

}
