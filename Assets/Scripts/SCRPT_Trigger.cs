using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_Trigger : MonoBehaviour
{
    public GameObject player;
    public GameObject Oni;
    [SerializeField] bool alreadyPlayed;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Oni.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alreadyPlayed)
        {
            if (collision.CompareTag("Player"))
            {
                alreadyPlayed = true;
                Oni.gameObject.SetActive(true);
            }
        }
    }
}
