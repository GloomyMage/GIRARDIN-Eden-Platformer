using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCRPT_Button_Manager : MonoBehaviour
{

    public SCRPT_AudioManager AudioManager;

    public void ButtonPlay()
    {
        SceneManager.LoadScene("SCN_Level_1");
    }

    public void MainMenu() 
    {
        SceneManager.LoadScene("SCN_Main_Menu");
    }

    public void ButtonOptions()
    {
        AudioManager.PlaySFX(AudioManager.SFXButton);
        Debug.Log("What's up?");
    }



    public void ButtonQuit()
    {
        Debug.Log("Goodbye!");
        Application.Quit();
    }


}
