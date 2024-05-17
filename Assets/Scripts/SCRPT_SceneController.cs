using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCRPT_SceneController : MonoBehaviour
{
    public static SCRPT_SceneController instance;
    [SerializeField] Animator transitionAnim;
    [SerializeField] SCRPT_Player_Movement CM;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel() 
    {
        StartCoroutine(LoadLevel());

    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadSceneAsync(SceneName);
    }    

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
       CM.CanMove = false;
        yield return new WaitForSeconds(1);
        CM.CanMove = true;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        
        transitionAnim.SetTrigger("Start");
    }
}
