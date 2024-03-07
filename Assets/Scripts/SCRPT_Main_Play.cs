using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCRPT_Button_Play : MonoBehaviour
{
    public void ButtonPress()
    {
        SceneManager.LoadScene("SCN_Level_One");
    }
}
