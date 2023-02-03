using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTransitionScript : MonoBehaviour
{
    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void DoorTransitionIn()
    {
        animator.SetTrigger("TransitionIn");
    }

    public void DoorTransitionOut()
    {
        PlayerManager.inst.PlayerInteract.ExitDoor();

        animator.SetTrigger("TransitionOut");
    }

}
