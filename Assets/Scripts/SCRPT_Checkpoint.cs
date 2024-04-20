using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_Checkpoint : MonoBehaviour
{

    SCRPT_GameController gameController;
    public SCRPT_AudioManager AudioManager;
    public Transform respawnPoint;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D coll;

    private void Start()
    {
        Color col = spriteRenderer.color;
        col.r = 1f; col.g = 0f; col.b = 0f;
        spriteRenderer.color = col;
    }

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<SCRPT_GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.PlaySFX(AudioManager.SFXCheckpoint);
            gameController.UpdateCheckpoint(respawnPoint.position);
            ColorChange();
            coll.enabled = false;
        }
    }

    private void ColorChange()
    {
        Color col = spriteRenderer.color;
        col.r = 0f; col.g = 1f; col.b = 0f;
        spriteRenderer.color = col;
    }
}
