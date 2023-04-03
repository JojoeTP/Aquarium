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
        animator.SetBool("DirectorTransitionOut",false);
        animator.SetBool("DirectorTransitionIn",true);
    }

    public void AIDirectorTransitionOut()
    {
        AiDirectorController.inst.DirectorTransition();
        animator.SetBool("DirectorTransitionIn",false);
        animator.SetBool("DirectorTransitionOut",true);
    }
}