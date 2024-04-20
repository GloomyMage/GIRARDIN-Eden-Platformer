using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_FinishPoint : MonoBehaviour
{
    [SerializeField] bool goNextLevel;
    [SerializeField] string levelName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (goNextLevel)
            {
                SCRPT_SceneController.instance.NextLevel();
            }
            else
            {
                SCRPT_SceneController.instance.LoadScene(levelName);
            }
            
        }
    }
}
