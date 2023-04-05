using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTransition : MonoBehaviour
{
    public static TutorialTransition inst;
    public Animator animator;
    public Tutorial tutorial;
    private void Awake()
    {
        inst = this;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TutorialFadeIn()
    {
        animator.SetTrigger("TutorialFadeIn");
        PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.CONVERSATION;
    }

    public void TutorialFadeOut()
    {
        animator.SetTrigger("TutorialFadeOut");
        PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;
    }
    public void OpenTutorial()
    {
        tutorial.ShowTutorialImage();
    }

    public void CloseTutorial()
    {
        tutorial.CloseTutorialImage();
    }
}
