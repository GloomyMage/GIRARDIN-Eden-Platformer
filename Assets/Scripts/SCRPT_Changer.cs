using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCRPT_Changer : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public bool main;
    [SerializeField] bool alreadyPlayed;
    [SerializeField] bool alreadyPlayed2;

    void Awake()
    {
        object1.gameObject.SetActive(true);
        object2.gameObject.SetActive(false);
        main = false;
        alreadyPlayed = false;
        alreadyPlayed2 = false;
    }

    private void Update()
    {
        if (!alreadyPlayed)
        {
            if (!object1.activeSelf)
            {
                alreadyPlayed = true;
                StartCoroutine("Change");

            }
        }
        if (!alreadyPlayed2)
        {
            if (!object2.activeSelf && main == true)
            {
                alreadyPlayed2 = true;
                StartCoroutine("Transition");
            }
        }
    }


    private IEnumerator Change()
    {
        yield return new WaitForSeconds(2f);
        main = true;
        object2.gameObject.SetActive(true);
    }

    private IEnumerator Transition()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SCN_Credits");
    }


}
