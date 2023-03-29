using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    enum VolumeType
    {
        Master,
        SFX,
        BGM,
    }

    [SerializeField] VolumeType volumeType;

    Slider volumeSlider;
    void Start()
    {
        volumeSlider = GetComponent<Slider>();
        volumeSlider.onValueChanged.AddListener((f) => {OnChangeVolume();});
    }

    void Update()
    {
        UpdateVolumeSlider();
    }

    void UpdateVolumeSlider()
    {
        switch(volumeType)
        {
            case VolumeType.Master :
                volumeSlider.value = SoundManager.Inst.masterVolume;
                break;
            case VolumeType.SFX :
                volumeSlider.value = SoundManager.Inst.SFXVolume;
                break;
            case VolumeType.BGM :
                volumeSlider.value = SoundManager.Inst.BGMVolume;
                break;
        }
    }

    void OnChangeVolume()
    {
        switch(volumeType)
        {
            case VolumeType.Master :
                SoundManager.Inst.masterVolume = volumeSlider.value;
                break;
            case VolumeType.SFX :
                SoundManager.Inst.SFXVolume = volumeSlider.value;
                break;
            case VolumeType.BGM :
                SoundManager.Inst.BGMVolume = volumeSlider.value;
                break;
        }
    }
}
