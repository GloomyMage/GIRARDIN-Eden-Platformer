using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SCRPT_Phoenix_Game : MonoBehaviour
{
    Vector2 CheckPointPos;
    Rigidbody2D playerRb;

    SCRPT_Camera_Control cameraController;
    public SCRPT_ParticleController particleController;
    public SCRPT_AudioManager AudioManager;
    public SCRPT_Phoenix player;

    public Light2D GIntensity;
    public Light2D LIntensity;

    private void Awake()
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SCRPT_Camera_Control>();
    }


    // Start is called before the first frame update
    void Start()
    {
        CheckPointPos = transform.position;
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Void"))
        {
            Die();
        }


    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        CheckPointPos = pos;
    }

    public void Die()
    {
    
        AudioManager.PlaySFX(AudioManager.SFXDeath);
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.velocity = new Vector2(0, 0);
        playerRb.simulated = false;
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = CheckPointPos;
        transform.localScale = new Vector3(1, 1, 1);
        playerRb.simulated = true;
    }
}
