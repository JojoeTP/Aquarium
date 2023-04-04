using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public GameObject playerLight;
    public bool isLight = false;

    void Start()
    {
        InputSystemManager.Inst.onLight += OnLight;
    }

    public void OnLight(){
        if(PlayerManager.inst.playerAnimator.GetBool("Lampitem") == false)
            return;

        if(PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.NONE)
            return;
        
        if(!isLight)
        {
            ToggleLight(true);
            PlayerManager.inst.playerAnimator.SetBool("LampLight",true);
            isLight = true;
        }
        else
        {
            ToggleLight(false);
            PlayerManager.inst.playerAnimator.SetBool("LampLight",false);
            isLight = false;
        }
    }

    public void ToggleLight(bool enable)
    {
        print("Toggle");
        playerLight.SetActive(enable);
    }
}
