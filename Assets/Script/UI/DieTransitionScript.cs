using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieTransitionScript : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void DieTransitionIn()
    {
        animator.SetTrigger("DieTransitionIn");
    }

    public void DieTransitionOut()
    {
        //destroy ai
        //Move player to respawn point
        AiMermaidController.inst.RespawnPlayer();
        AiDirectorController.inst.RespawnPlayer();

        animator.SetTrigger("DieTransitionOut");
    }
}