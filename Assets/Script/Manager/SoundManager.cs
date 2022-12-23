using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Inst;

    [SerializeField] List<AudioSource> BGMSources = new List<AudioSource>();
    [SerializeField] List<AudioSource> SFXSources = new List<AudioSource>();

    bool isCoroutineRunning = false;
    int i = 0;

    private void Awake() 
    {
        Inst = this;        
    }

    void Start()
    {
        // if(!isCoroutineRunning)
        // {
        //     i++;
        //     StartCoroutine(TestCoroutine());
        // }

        StartCoroutine(TestCoroutine());
    }

    float time;

    void Update()
    {
        time = Time.time;
    }

    IEnumerator TestCoroutine()
    {
        // while(true)
        // {
        //     float time = Time.time;

        //     print($"Test {i} : {time} ");
        //     isCoroutineRunning = true;
        //     yield return null;
            
        // }


        while(!isCoroutineRunning && i < 5)
        {
            i++;
            print($"Test {i} : {time} ");
            isCoroutineRunning = true;
            yield return new WaitForSeconds(2f);
            isCoroutineRunning = false;
            print("Turn to false");
        }


        print($"after Finish {i} : {time} ");
        yield return new WaitForSeconds(3f);
        print($"Befor Finish {i} : {time} ");

    }

    void OnChangeMasterVolume(float volume)
    {

    }
    
    void OnChangeBGMVolume(float volume)
    {

    }

    void OnChangeSFXVolume(float volume)
    {

    }
}
