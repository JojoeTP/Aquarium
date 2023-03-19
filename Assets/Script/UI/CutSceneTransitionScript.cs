using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTransitionScript : MonoBehaviour
{
    Animator animator;

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
        //animator.SetTrigger("CutSceneTransitionOut");
        PlayerManager.inst.PlayerInteract.Interacting();
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
