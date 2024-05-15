using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCRPT_Portal : MonoBehaviour
{

    public NewControls controls;
    private InputAction _movementDoor;

    public Transform destination;
    private bool enterAllowed;
    GameObject player;
    Animation anim;
    Rigidbody2D playerRb;


    private void OnEnable()
    {
        _movementDoor = controls.Player.Door;
        _movementDoor.Enable();

        _movementDoor.started += Door;


    }

    private void OnDisable()
    {
        _movementDoor.started -= Door;
        _movementDoor.Disable();

    }

    private void Awake()
    {
        controls = new NewControls();

        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animation>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enterAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enterAllowed = false;
        }
    }

    private void Door(InputAction.CallbackContext context)
    {
        if (enterAllowed)
        {
            StartCoroutine(DoorIn());
        }
    }

    IEnumerator DoorIn()
    {
        playerRb.simulated = false;
        anim.Play("DoorIn");
        StartCoroutine(MoveInDoor());
        yield return new WaitForSeconds(1f);
        player.transform.position = destination.transform.position;
        playerRb.velocity = Vector2.zero;
        anim.Play("DoorOut");
        yield return new WaitForSeconds(1f);
        playerRb.simulated = true;
    }

    IEnumerator MoveInDoor()
    {
        float timer = 0;
        while (timer < 1f) 
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, 3 * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}

