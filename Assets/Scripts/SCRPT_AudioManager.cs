using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SCRPT_AudioManager : MonoBehaviour
{
    [Header("----------===== Audio Source =====----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource musicSourceBis;
    [SerializeField] AudioSource SFXSource;

    [Header("----------===== Audio Clip =====----------")]
    public AudioClip MusicLVL1;
    public AudioClip MusicLVL2;
    public AudioClip MusicLVL3;
    public AudioClip MusicLVL4;
    public AudioClip MusicLVL5;
    public AudioClip MusicLVL6;
    public AudioClip MusicMenu;
    public AudioClip SFXJump;
    public AudioClip SFXLanding;
    public AudioClip SFXTransparent;
    public AudioClip SFXDeath;
    public AudioClip SFXCheckpoint;
    public AudioClip SFXButton;
    public AudioClip SFXWind;
    public AudioClip SFXRain;
    public AudioClip SFXEnemy;

    public static SCRPT_AudioManager instance;


    //private void Awake()
    //{

    //    if (instance == null)
    //    {

    //        instance = this;
    //        DontDestroyOnLoad(gameObject);

    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }

    //}


    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "SCN_Main_Menu")
        {
            musicSource.clip = MusicMenu;
            musicSource.Play();
        }
        else if (sceneName == "SCN_Level_1")
        {
            musicSource.clip = MusicLVL1;
            musicSource.Play();
        }
        else if (sceneName == "SCN_Level_2")
        {
            musicSource.clip = MusicLVL2;
            musicSource.Play();
        }
        else if (sceneName == "SCN_Level_3")
        {
            musicSource.clip = MusicLVL3;
            musicSource.Play();
            musicSourceBis.clip = SFXWind;
            musicSourceBis.Play();
        }
        else if (sceneName == "SCN_Level_4")
        {
            musicSource.clip = MusicLVL4;
            musicSource.Play();
            musicSourceBis.clip = SFXRain;
            musicSourceBis.Play();
        }
        else if (sceneName == "SCN_Level_5")
        {
            musicSource.clip = MusicLVL5;
            musicSource.Play();
        }
        else if (sceneName == "SCN_Level_6")
        {
            musicSource.clip = MusicLVL6;
            musicSource.Play();
        }
    }

    //static public void PlayMusic()
    //{
    //    Scene currentScene = SceneManager.GetActiveScene();
    //    string sceneName = currentScene.name;

    //    if (instance != null)
    //    {
    //        if (instance.musicSource != null)
    //        {
    //            instance.musicSource.Stop();
    //            if (sceneName == "SCN_Main_Menu")
    //            {
    //                instance.musicSource.clip = instance.MusicMenu;
    //                instance.musicSource.Play();
    //            }
    //            else if (sceneName == "SCN_Level_1")
    //            {
    //                instance.musicSource.clip = instance.MusicLVL1;
    //                instance.musicSource.Play();
    //            }
    //            else if (sceneName == "SCN_Level_2")
    //            {
    //                instance.musicSource.clip = instance.MusicLVL2;
    //                instance.musicSource.Play();
    //            }
    //            else if (sceneName == "SCN_Level_3")
    //            {
    //                instance.musicSource.clip = instance.MusicLVL3;
    //                instance.musicSource.Play();
    //            }
    //            else if (sceneName == "SCN_Level_4")
    //            {
    //                instance.musicSource.clip = instance.MusicLVL4;
    //                instance.musicSource.Play();
    //            }
    //            else if (sceneName == "SCN_Level_5")
    //            {
    //                instance.musicSource.clip = instance.MusicLVL5;
    //                instance.musicSource.Play();
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("Unavailable MusicPlayer component");
    //    }
    //}


    public void PlayMusic(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
