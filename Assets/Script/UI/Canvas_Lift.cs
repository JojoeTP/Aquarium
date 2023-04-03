using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Lift : MonoBehaviour
{
    void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void PlayButton()
    {
        SoundManager.Inst.InitializeUI(FMODEvent.inst.FModEventDictionary["UIButton"]);
    }
}
