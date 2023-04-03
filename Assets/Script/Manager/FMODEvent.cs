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
    [field : SerializeField] public List<FMODEventConfig> SFXList {get; private set;}
    [field : SerializeField] public List<FMODEventConfig> MonsterList {get; private set;}
    

    // [field : Header("UI")]
    [field : Header("BGM")]
    [field : SerializeField] public FMODEventConfig MainMenuMusic {get; private set;}
    [field : SerializeField] public List<FMODEventConfig> BGMList {get; private set;}

    public Dictionary<string,EventReference> FModEventDictionary = new Dictionary<string, EventReference>();
    void Awake() 
    {
        inst = this;    
    }

    void Start() 
    {
        foreach(var n in SFXList)
        {
            FModEventDictionary.Add(n.key,n.sound);    
        }

        foreach(var n in BGMList)
        {
            FModEventDictionary.Add(n.key,n.sound);    
        }

        foreach(var n in MonsterList)
        {
            FModEventDictionary.Add(n.key,n.sound);    
        }
    }
}

[Serializable]
public class FMODEventConfig
{
    public string key;
    public EventReference sound;
}
