using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Inst;

    [Header("Volume")]
    [Range(0,1)]
    public float masterVolume = 1;
    [Range(0,1)]
    public float BGMVolume = 1;
    [Range(0,1)]
    public float SFXVolume = 1;

    List<EventInstance> eventInstances;
    List<StudioEventEmitter> eventEmitters;

    EventInstance ambienceEventInstance;
    EventInstance BGMEventInstance;

    FMODEvent FMODEvent;

    void Awake() 
    {
        Inst = this; 

        Initilize();      
    }

    void Initilize()
    {
        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {

    }
    
    //Use 2d action event
    public void PlayOneShot(EventReference sound,Vector3 position)
    {
        RuntimeManager.PlayOneShot(sound,position);
    }

    //Use Timeline
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference,GameObject emitterObejct)
    {
        StudioEventEmitter emitter = emitterObejct.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }

    /// <summary>
    /// Use to play ambience, It will auto stop a ambience before
    /// </summary>
    public void InitializeAmbience(EventReference eventReference)
    {
        StopAmbience();

        ambienceEventInstance = CreateInstance(eventReference);
        ambienceEventInstance.start();
    }

    public void StopAmbience()
    {
        if(ambienceEventInstance.isValid())
            ambienceEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    /// <summary>
    /// Use to play BGM, It will auto stop a BGM before
    /// </summary>
    public void InitializeBGM(EventReference eventReference)
    {
        StopBGM();

        BGMEventInstance = CreateInstance(eventReference);
        BGMEventInstance.start();
    }

    public void StopBGM()
    {
        if(BGMEventInstance.isValid())
            BGMEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void SetAmbienceParameter(string parameterName,float parameterValue)
    {
        ambienceEventInstance.setParameterByName(parameterName,parameterValue);
    }

    void CleanUp()
    {
        foreach(var n in eventInstances)
        {
            n.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            n.release();
        }

        foreach(var n in eventEmitters)
        {
            n.Stop();
        }
    }

    void OnDestroy() 
    {
        CleanUp();    
    }


    
}