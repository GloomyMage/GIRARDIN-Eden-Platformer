using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SCRPT_Pause : MonoBehaviour
{
    public GameObject pauseMenu;

    public static bool GameIsPaused;

    void Update()
    {
        Test();
    }


    public void Test()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }

        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void Home()
    {
        SceneManager.LoadScene("SCN_main_Menu");
        Time.timeScale = 1;
    }

}
