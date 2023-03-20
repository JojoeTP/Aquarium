using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDirectorTransitionScript : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AIDirectorTransitionIn()
    {
        animator.SetTrigger("DirectorTransitionIn");
    }

    public void AIDirectorTransitionOut()
    {
        AiDirectorController.inst.DirectorTransition();
        animator.SetTrigger("DirectorTransitionOut");
    }
}