using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_InvisiblePlatform : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite_renderer;


    // Update is called once per frame
    void Update()
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
            Color col = sprite_renderer.color;
            col.a = 1;
            sprite_renderer.color = col;
        }
            else
            {
            Color col = sprite_renderer.color;
            col.a = 0;
            sprite_renderer.color = col;
        }
        }


    }

