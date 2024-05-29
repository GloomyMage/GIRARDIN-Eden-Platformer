using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class SCRPT_Invisible : MonoBehaviour
{
    public NewControls controls;
    private InputAction _movementInvisible;

    [SerializeField] SpriteRenderer sprite_renderer;


    private void Awake()
    {
        controls = new NewControls();

        Color col = sprite_renderer.color;
        col.a = 0;
        sprite_renderer.color = col;
    }

    public void OnEnable()
    {

        _movementInvisible = controls.Player.Invisible;
        _movementInvisible.Enable();

        _movementInvisible.performed += Invisible;
        _movementInvisible.canceled += Visible;


    }

    public void OnDisable()
    {

        _movementInvisible.performed -= Invisible;
        _movementInvisible.canceled -= Visible;
        _movementInvisible.Disable();

    }



    public void Invisible(InputAction.CallbackContext context)
    {

        Color col = sprite_renderer.color;
        col.a = 1;
        sprite_renderer.color = col;
    }

    public void Visible(InputAction.CallbackContext context)
    {
        Color col = sprite_renderer.color;
        col.a = 0;
        sprite_renderer.color = col;
    }



}

