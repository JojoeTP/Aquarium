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
        if(!isLight)
        {
            playerLight.SetActive(true);
            PlayerManager.inst.playerAnimator.SetBool("LampLight",true);
            isLight = true;
        }
        else
        {
            playerLight.SetActive(false);
            PlayerManager.inst.playerAnimator.SetBool("LampLight",false);
            isLight = false;
        }
    }
}
