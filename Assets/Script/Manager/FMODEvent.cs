using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

public class FMODEvent : MonoBehaviour
{
    public static FMODEvent inst;

    [field : Header("SFX")]
    [field : SerializeField] public FMODEventConfig playerMovementSFX {get; private set;}

    // [field : Header("UI")]
    [field : Header("BGM")]
    [field : SerializeField] public FMODEventConfig MainMenuMusic {get; private set;}

    void Awake() 
    {
        inst = this;    
    }
}

[Serializable]
public class FMODEventConfig
{
    public string key;
    public EventReference sound;
}
