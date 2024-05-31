using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SCRPT_SilentCheckpoint : MonoBehaviour
{

    SCRPT_GameController gameController;
    public Transform respawnPoint;
    [SerializeField] Collider2D coll;


    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<SCRPT_GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Checkpoint());

        }
    }

    IEnumerator Checkpoint()
    {
        yield return new WaitForSeconds(0.5f);
        gameController.UpdateCheckpoint(respawnPoint.position);
        coll.enabled = false;
    }
}
