using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvent : MonoBehaviour
{
    public static FMODEvent inst;

    [field : Header("SFX")]
    [field : SerializeField] public EventReference playerMovementSFX {get; private set;}

    // [field : Header("UI")]
    [field : Header("BGM")]
    [field : SerializeField] public EventReference MainMenuMusic {get; private set;}

    void Awake() 
    {
        inst = this;    
    }
}
