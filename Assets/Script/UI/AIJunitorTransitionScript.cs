using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIJunitorTransitionScript : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AIJunitorTransitionIn()
    {
        animator.SetBool("JunitorTransitionOut",false);
        animator.SetBool("JunitorTransitionIn",true);
    }

    public void AIJunitorTransitionOut()
    {
        AiJunitorController.inst.JunitorTransition();
        animator.SetBool("JunitorTransitionIn",false);
        animator.SetBool("JunitorTransitionOut",true);
    }
}