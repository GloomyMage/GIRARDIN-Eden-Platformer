using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SCRPT_Pause : MonoBehaviour
{
    public GameObject pauseMenu;

    private InputAction _movementEscape;
    public NewControls controls;
    public static bool GameIsPaused;

    private void OnEnable()
    {
        _movementEscape = controls.Player.Escape;
        _movementEscape.Enable();

        _movementEscape.started += Escape;


    }

    private void OnDisable()
    {
        _movementEscape.started -= Escape;
        _movementEscape.Disable();

    }

    private void Escape(InputAction.CallbackContext context)
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

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Home()
    {
        SceneManager.LoadScene("SCN_main_Menu");
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

}
