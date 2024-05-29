using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public StoryScene currentScene;
    public BottomBarController bottomBar;
    public bool me;
    public SCRPT_Player_Movement player;
    public SCRPT_AudioManager audioManager;
    string sceneName;
    public int count;


    public GameObject text;
    public GameObject image;
    public GameObject Speaker1;
    public GameObject Speaker2;



    void Start()
    {
        bottomBar.PlayScene(currentScene);

                    me = true;
        count = 0;
    }

    void Update()
    {
        if (me == true)
        {
            Speaker1.gameObject.SetActive(true);
            Speaker2.gameObject.SetActive(false);
        }
        else
        {
            Speaker1.gameObject.SetActive(false);
            Speaker2.gameObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetButtonDown("Chat"))
        {
            if (bottomBar.IsCompleted())
            {
                if (bottomBar.IsLastSentence())
                {
                    text.gameObject.SetActive(false);
                    image.gameObject.SetActive(false);
                    player.canMove = true;
                    audioManager.PlaySFX(audioManager.SFXButton);
                    Destroy(gameObject);
                }
                else
                {
                    bottomBar.PlayNextSentence();
                    audioManager.PlaySFX(audioManager.SFXButton);
                    me = !me;
                    count ++;
                }
            }
        }
        else if (Input.GetButtonDown("Submit"))
        {
            text.gameObject.SetActive(false);
            image.gameObject.SetActive(false);
            player.canMove = true;
            Destroy(gameObject);
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }
}
