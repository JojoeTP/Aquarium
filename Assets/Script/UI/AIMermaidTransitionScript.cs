using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMermaidTransitionScript : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AIMermaidTransitionIn()
    {
        animator.SetTrigger("MermaidTransitionIn");
    }

    public void AIMermaidTransitionOut()
    {
        AiMermaidController.inst.MermaidTransition();
        animator.SetTrigger("MermaidTransitionOut");
    }
}