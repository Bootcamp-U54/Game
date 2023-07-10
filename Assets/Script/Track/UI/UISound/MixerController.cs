using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{

   [SerializeField] private AudioMixer myAudioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    const string MIXER_MUSIC = "MusicVolume";
    const string MIXER_SFX = "SFXVolume";

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
      
    }
    private void Start()
    {
        LoadSoundMusic();
        LoadSoundSFX();
    }

    public void SetMusicVolume(float sliderValue)
    {
        myAudioMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(MIXER_MUSIC, sliderValue);
        LoadSoundMusic();
    }
    public void SetSfxVolume(float sliderValue)
    {
        myAudioMixer.SetFloat(MIXER_SFX, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(MIXER_SFX, sliderValue);
        LoadSoundSFX();

    }

    public void LoadSoundMusic()
    {
        musicSlider.value = PlayerPrefs.GetFloat(MIXER_MUSIC);
    
    }
      public void LoadSoundSFX()
    {
       
        sfxSlider.value = PlayerPrefs.GetFloat(MIXER_SFX);
    }

}
