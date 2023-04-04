using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRedHoodTransitionScript : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AIRedHoodTransitionIn()
    {
        animator.SetBool("RedHoodTransitionOut",false);
        animator.SetBool("RedHoodTransitionIn",true);
    }

    public void AIRedHoodTransitionOut()
    {
        AiRedHoodController.inst.RedHoodTransition();
        animator.SetBool("RedHoodTransitionIn",false);
        animator.SetBool("RedHoodTransitionOut",true);
    }
}