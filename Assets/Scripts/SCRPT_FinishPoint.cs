using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCRPT_FinishPoint : MonoBehaviour
{
    [SerializeField] bool goNextLevel;
    [SerializeField] string levelName;
 [SerializeField] bool alreadyPlayed;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alreadyPlayed){
        if (collision.CompareTag("Player"))
        {
alreadyPlayed = true;
            if (goNextLevel)
            {
                UnlockNewLevel();
                SCRPT_SceneController.instance.NextLevel();
            }
            else
            {
                SCRPT_SceneController.instance.LoadScene(levelName);
            }
            
        }
    }
}

    private void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
