using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI inst;
    public Animator animator;
    public Tutorial tutorial;
    private void Awake()
    {
        inst = this;
    }

    public void TutorialFadeIn()
    {
        animator.SetTrigger("TutorialFadeIn");
    }

    public void TutorialFadeOut()
    {
        animator.SetTrigger("TutorialFadeOut");
    }
}
