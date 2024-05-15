using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCRPT_Go_Through : MonoBehaviour
{
    public NewControls controls;
    private InputAction _movementInvisible;


    private PlatformEffector2D effector;
    public float waitTime;

    private void Awake()
    {
        controls = new NewControls();

        effector = GetComponent<PlatformEffector2D>();
        effector.rotationalOffset = 0;
    }

    private void OnEnable()
    {

        _movementInvisible = controls.Player.Invisible;
        _movementInvisible.Enable();

        _movementInvisible.performed += Invisible;
        _movementInvisible.canceled += Visible;


    }

    private void OnDisable()
    {

        _movementInvisible.performed -= Invisible;
        _movementInvisible.canceled -= Visible;
        _movementInvisible.Disable();

    }

    void Invisible(InputAction.CallbackContext context)
    {
            effector.rotationalOffset = 180f;
        }

    private void Visible(InputAction.CallbackContext context)
    {
            effector.rotationalOffset = 0;
    }

  
}
