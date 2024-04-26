using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SCRPT_Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
       
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Home()
    {
        SceneManager.LoadScene("SCN_main_Menu");
        Time.timeScale = 1;
    }

}
