using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SCRPT_AudioManager : MonoBehaviour
{
    [Header("----------===== Audio Source =====----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------===== Audio Clip =====----------")]
    public AudioClip MusicLVL1;
    public AudioClip MusicLVL2;
    public AudioClip MusicLVL3;
    public AudioClip MusicLVL4;
    public AudioClip MusicLVL5;
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

    // Scene Manager
    string sceneName;

    public static SCRPT_AudioManager instance;


    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "SCN_Level_One")
        {
            musicSource.clip = MusicLVL1;
            musicSource.Play();
        }
        else if (sceneName == "SCN_Level_Two")
        {
            musicSource.clip = MusicLVL2;
            musicSource.Play();
        }
        else if (sceneName == "SCN_Level_Three")
        {
            musicSource.clip = MusicLVL3;
            musicSource.Play();
        }
        else if (sceneName == "SCN_Level_Four")
        {
            musicSource.clip = MusicLVL4;
            musicSource.Play();
        }
        else if (sceneName == "SCN_Level_Five")
        {
            musicSource.clip = MusicLVL5;
            musicSource.Play();
        }
        else if (sceneName == "SCN_Main_Menu")
        {
            musicSource.clip = MusicMenu;
            musicSource.Play();
        }
    }

    //private void Awake()
    //{
    //    Scene currentScene = SceneManager.GetActiveScene();
    //    sceneName = currentScene.name;


    //    if (instance == null)
    //    {

    //       if (sceneName == "SCN_Level_One")
    //        {
    //            SCRPT_AudioManager.instance.GetComponent<AudioSource>().Pause();
    //           musicSource.clip = MusicLVL1;
    //           musicSource.Play();
    //      }
    //       else if (sceneName == "SCN_Level_Two")
    //        {
    //            SCRPT_AudioManager.instance.GetComponent<AudioSource>().Pause();
    //           musicSource.clip = MusicLVL2;
    //            musicSource.Play();
    //        }
    //        else if (sceneName == "SCN_Level_Three")
    //        {
    //            SCRPT_AudioManager.instance.GetComponent<AudioSource>().Pause();
    //          musicSource.clip = MusicLVL3;
    //           musicSource.Play();
    //       }
    //       else if (sceneName == "SCN_Level_Four")
    //       {
    //           SCRPT_AudioManager.instance.GetComponent<AudioSource>().Pause();
    //          musicSource.clip = MusicLVL4;
    //           musicSource.Play();
    //        }
    //        else if (sceneName == "SCN_Level_Five")
    //       {
    //          SCRPT_AudioManager.instance.GetComponent<AudioSource>().Pause();
    //           musicSource.clip = MusicLVL5;
    //            musicSource.Play();
    //       }
    //        else if (sceneName == "SCN_Main_Menu")
    //       {
    //            SCRPT_AudioManager.instance.GetComponent<AudioSource>().Pause();
    //           musicSource.clip = MusicMenu;
    //          musicSource.Play();
    //       }

    //       instance = this;
    //      DontDestroyOnLoad(gameObject);
    //   }
    //   else
    //    {
    //       Destroy(gameObject);
    //    }

    //}

    //private void Update()
    //{
    //    Scene currentScene = SceneManager.GetActiveScene();
    //    sceneName = currentScene.name;


    //   if (sceneName == "SCN_Level_One")
    //    {
    //       SCRPT_AudioManager.instance.GetComponent<AudioSource>().Pause();
    //      musicSource.clip = MusicLVL1;
    //       musicSource.Play();
    //  }
    //   else if (sceneName == "SCN_Level_Two")
    //   {
    //       SCRPT_AudioManager.instance.GetComponent<AudioSource>().Pause();
    //       musicSource.clip = MusicLVL2;
    //       musicSource.Play();
    //   }
    //   else if (sceneName == "SCN_Level_Three")
    //   {
    //       SCRPT_AudioManager.instance.GetComponent<AudioSource>().Pause();
    //       musicSource.clip = MusicLVL3;
    //        musicSource.Play();
    //   }
    //   else if (sceneName == "SCN_Level_Four")
    //   {
    //      SCRPT_AudioManager.instance.GetComponent<AudioSource>().Pause();
    //      musicSource.clip = MusicLVL4;
    //       musicSource.Play();
    //   }
    //    else if (sceneName == "SCN_Level_Five")
    //    {
    //       SCRPT_AudioManager.instance.GetComponent<AudioSource>().Pause();
    //      musicSource.clip = MusicLVL5;
    //        musicSource.Play();
    //    }
    //    else if (sceneName == "SCN_Main_Menu")
    //    {
    //       SCRPT_AudioManager.instance.GetComponent<AudioSource>().Pause();
    //      musicSource.clip = MusicMenu;
    //       musicSource.Play();
    //    }

    //}

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
