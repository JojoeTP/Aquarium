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

    Bus masterBus;
    Bus BGMBus;
    Bus SFXBus;

    List<EventInstance> eventInstances;
    List<StudioEventEmitter> eventEmitters;

    EventInstance ambienceEventInstance;
    EventInstance BGMEventInstance;
    EventInstance DialogueEventInstance;
    EventInstance DialogueBGMEventInstance;
    

    void Awake() 
    {
        Inst = this; 

        Initilize();      
    }

    void Initilize()
    {
        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();

        masterBus = RuntimeManager.GetBus("bus:/");
        BGMBus = RuntimeManager.GetBus("bus:/BGM");
        SFXBus = RuntimeManager.GetBus("bus:/SFX");
    }

    void Start()
    {

    }

    void Update()
    {
        masterBus.setVolume(masterVolume);
        BGMBus.setVolume(BGMVolume);
        SFXBus.setVolume(SFXVolume);
    }

    public void MuteBGM()
    {
        BGMBus.setMute(true);
    }

    public void ContinuePlayBGM()
    {
        BGMBus.setMute(false);
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
    public void InitializeDialogueSound(EventReference eventReference)
    {
        DialogueEventInstance = CreateInstance(eventReference);
        DialogueEventInstance.start();
    }

    public void InitializeDialogueBGM(EventReference eventReference)
    {
        StopDialogueBGM();
        DialogueBGMEventInstance = CreateInstance(eventReference);
        DialogueBGMEventInstance.start();
    }

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

    public void StopDialogueBGM()
    {
        if(DialogueBGMEventInstance.isValid())
            DialogueBGMEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    
    public void StopDialogue()
    {
        if(DialogueEventInstance.isValid())
            DialogueEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void SetAmbienceParameter(string parameterName,float parameterValue)
    {
        ambienceEventInstance.setParameterByName(parameterName,parameterValue);
    }

    public void CleanUp()
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

        ambienceEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        ambienceEventInstance.release();

        BGMEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        BGMEventInstance.release();
    }

    void OnDestroy() 
    {
        CleanUp();    
    }
    
}