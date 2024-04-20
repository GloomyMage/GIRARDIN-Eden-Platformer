using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SCRPT_VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicV"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetMusicVolume();
            SetSFXVolume();
        }
    }


    public void SetMasterVolume()
    {
        float volumeMaster = masterSlider.value;
        myMixer.SetFloat("MasterV", Mathf.Log10(volumeMaster) * 20);
        PlayerPrefs.SetFloat("MasterV", volumeMaster);
    }

    public void SetMusicVolume()
    {
        float volumeMusic = musicSlider.value;
        myMixer.SetFloat("MusicV", Mathf.Log10(volumeMusic)*20);
        PlayerPrefs.SetFloat("MusicV", volumeMusic);
    }

    public void SetSFXVolume()
    {
        float volumeSFX = SFXSlider.value;
        myMixer.SetFloat("SFXV", Mathf.Log10(volumeSFX) * 20);
        PlayerPrefs.SetFloat("SFXV", volumeSFX);
    }

    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterV");
        musicSlider.value = PlayerPrefs.GetFloat("MusicV");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXV");

        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
}
