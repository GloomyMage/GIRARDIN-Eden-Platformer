using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SCRPT_Checkpoint : MonoBehaviour
{

    SCRPT_GameController gameController;
    public SCRPT_AudioManager AudioManager;
    public Transform respawnPoint;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D coll;
    public Animator animator;
    GameObject flag;
    Animation anim;
    public Light2D Intensity;

    private void Start()
    {
        Color col = spriteRenderer.color;
        col.r = 1f; col.g = 1f; col.b = 1f;
        spriteRenderer.color = col;
        Intensity.intensity = 0f;
        anim = flag.GetComponent<Animation>();
    }

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<SCRPT_GameController>();
        flag = GameObject.FindGameObjectWithTag("Flag");
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
        animator.SetBool("Activated", true);
        AudioManager.PlaySFX(AudioManager.SFXCheckpoint);
        yield return new WaitForSeconds(0.5f);
        gameController.UpdateCheckpoint(respawnPoint.position);
        LightChange();
        animator.SetBool("Activated", false);
        coll.enabled = false;
    }

    private void LightChange()
    {
        Intensity.intensity = 1f;
    }
}
