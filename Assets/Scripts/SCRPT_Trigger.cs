using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_Trigger : MonoBehaviour
{
    public GameObject player;
    public GameObject Oni;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Oni.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Oni.gameObject.SetActive(true);
        }
    }
}
