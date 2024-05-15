using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_NPC : MonoBehaviour
{
    public SpriteRenderer sprite_renderer;
    public Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        Stance();
    }

    private void Stance()
    {
        if (transform.position.x > playerTransform.position.x)
        {
            sprite_renderer.flipX = false;
        }
        else if (transform.position.x < playerTransform.position.x)
        {
            sprite_renderer.flipX = true;
        }
    }
}
