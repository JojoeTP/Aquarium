using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
using System;

public class CameraShake : MonoBehaviour
{
    public static CameraShake inst; 
    CinemachineVirtualCamera vCam;
    CinemachineBasicMultiChannelPerlin channelPerlin;
    [SerializeField] float amplitude = 5;
    [SerializeField] float shakeSpeed = 2;

    float currentAmplitude = 0;
    float elapsedTime = 0;

    void Awake()
    {
        inst = this;
    }

    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();    
        channelPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        ProcessShake();

        channelPerlin.m_AmplitudeGain = currentAmplitude;
    }

    public void Play()
    {
        // elapsedTime = shaketime;
        currentAmplitude = amplitude;
    }

    void ProcessShake()
    {
        Shake();
    }

    void Shake()
    {
        // elapsedTime -= Time.deltaTime;
        
        if(currentAmplitude > 0)
        {
            currentAmplitude -= Time.deltaTime * shakeSpeed;
        }
        else
        {
            currentAmplitude = 0;
        }
    }
}

#if UNITY_EDITOR
    [CustomEditor(typeof(CameraShake))]
    public class CameraTester : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            CameraShake actionActive = (CameraShake)target;

            if (GUILayout.Button("Play"))
            {
                actionActive.Play();
            }
        }
    }
#endif
