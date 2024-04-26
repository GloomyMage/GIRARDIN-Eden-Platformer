using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCRPT_Button_Manager : MonoBehaviour
{
    public void ButtonPlay()
    {
        SceneManager.LoadScene("SCN_Level_1");
    }

    public void ButtonOptions()
    {
        Debug.Log("What's up?");
    }

    public void ButtonQuit()
    {
        Debug.Log("Goodbye!");
        Application.Quit();
    }


}
