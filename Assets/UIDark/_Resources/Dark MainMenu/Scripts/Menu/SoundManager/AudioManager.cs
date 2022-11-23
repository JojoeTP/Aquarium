using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
 
public class AudioManager : MonoBehaviour {
 
    public AudioMixer audioMixer;
    [Space(10)]
    public Slider musicSlider;
    public Slider sfxSlider;
     
        // Use this for initialization
        void Start () {
            //load from sound music slider
            audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume", 0));
            audioMixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("sfxVolume", 0));
        }
 
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }
    public void SetSfxVolume (float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume);
    }
    private void OnEnable()
    {
        //load playerpref and apply to slider UI
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0);
 
        //load playerpref for music and sfx volume
        //SetMusicVolume(PlayerPrefs.GetFloat("musicVolume", 0));
        //SetSfxVolume(PlayerPrefs.GetFloat("sfxVolume", 0));
 
    }
 
    private void OnDisable()
    {
        float musicVolume = 0;
        float sfxVolume = 0;
 
        audioMixer.GetFloat("musicVolume", out musicVolume);
        audioMixer.GetFloat("sfxVolume", out sfxVolume);
 
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();
    }
 
}