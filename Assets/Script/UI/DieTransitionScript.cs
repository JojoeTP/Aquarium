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
        AiRedHoodController.inst.RespawnPlayer();

        if(PlayerManager.inst.playerState == PlayerManager.PLAYERSTATE.HIDING)
        {
            foreach(var n in PlayerManager.inst.playerSprite)
            {
                n.SetActive(true);
            }
            PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;

            if(PlayerManager.inst.PlayerLight.isLight)
                    PlayerManager.inst.PlayerLight.ToggleLight(true);
        } 

        animator.SetTrigger("DieTransitionOut");
    }
}