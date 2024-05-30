using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SCRPT_Player_Movement : MonoBehaviour
{

    // Other Scripts
    [Header("----------===== Other Scripts =====----------")]
    public Light2D Intensity;
    public SCRPT_Chase Ghost;
    public SCRPT_Chase Ghost2;
    public SCRPT_Chase Ghost3;
    public SCRPT_ParticleController ParticleController;
    public SCRPT_AudioManager AudioManager;
    public NewControls controls;

    // Inputs
    [Header("----------=====  Inputs  =====----------")]
    private InputAction _movementAction;
    private InputAction _movementJump;
    private InputAction _movementInvisible;
    private InputAction _movementDoor;
    private InputAction _movementTalk;
    private InputAction _movementEscape;

    // Attributes
    [Header ("----------=====  Attributes  =====----------")]
    [SerializeField] Animator Player_Animator;
    [SerializeField] SpriteRenderer sprite_renderer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] CapsuleCollider2D capsule_collider;
    public bool phantom = false;
    public bool canMove = false;

    // Knockback
    [Header("----------===== Knockback =====----------")]
    public float KBForce = 5;
    public float KBCounter = 0;
    public float KBTotalTime = 0.2f;
    public bool KnockFromRight;

    // Set speed movement speed
    [Header("----------===== Speed =====----------")]
    [SerializeField] float movement_speed = 6f;
    private float moveInput;

    // Jump
    [Header("----------===== Jump =====----------")]
    [SerializeField] float JumpAmount = 10f;

    [SerializeField] bool Jumping = false;
    public bool isGrounded;
    public Transform Ground_Detector;
    public float checkRadius;
    public LayerMask whatIsGround;

    public float jumpTimeCounter;
    [SerializeField] float jumpTime = 0.33f;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;


    // Scene Manager
    string sceneName;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Scene Manager
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        canMove = false;

        if (sceneName == "SCN_Level_2")
        {
            Player_Animator.SetBool("IsJumping", true);
        }
        else
        {
            Player_Animator.SetBool("IsJumping", false);
        }
    }

    private void Awake()
    {
        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SCRPT_AudioManager>();
        controls = new NewControls();
        Physics2D.IgnoreLayerCollision(7, 9, true);
        Physics2D.IgnoreLayerCollision(9, 10, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            lateral();
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(Ground_Detector.position, checkRadius, whatIsGround);

        Player_Animator.SetFloat("Falling", (rb.velocity.y));

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (canMove)
        {

            Player_Animator.SetFloat("Speed", Mathf.Abs(moveInput));

            if (KBCounter <= 0)
            {
                moveInput = Input.GetAxis("Horizontal");
                rb.velocity = new Vector2(moveInput * movement_speed, rb.velocity.y);

            }

            else
            {
                if (KnockFromRight == true)
                {
                    rb.velocity = new Vector2(-KBForce, KBForce);
                }
                else if (KnockFromRight == false)
                {
                    rb.velocity = new Vector2(KBForce, KBForce);
                }
            }

            KBCounter -= Time.deltaTime;

            if (moveInput > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (moveInput < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            Vector2 moveDir = _movementAction.ReadValue<Vector2>();



            


            if (isGrounded == true && Input.GetButtonDown("Jump"))
            {
                AudioManager.PlaySFX(AudioManager.SFXJump);
                Jumping = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = (Vector2.up * JumpAmount);
            }

            if (Input.GetButton("Jump") && Jumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = (Vector2.up * JumpAmount);
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    Jumping = false;
                }


            }

            if (Input.GetButton("Jump"))
            {
                Jumping = false;
            }
        }
    }

    private void OnEnable()
    {
        _movementAction = controls.Player.Movement;
        _movementAction.Enable();

        _movementJump = controls.Player.Jump;
        _movementJump.Enable();

        _movementInvisible = controls.Player.Invisible;
        _movementInvisible.Enable();

        _movementDoor = controls.Player.Door;
        _movementDoor.Enable();

        _movementTalk = controls.Player.Talk;
        _movementTalk.Enable();

       // _movementJump.started += Jump;
        _movementJump.performed += HoldJump;
        _movementJump.canceled += OffJump;

        _movementInvisible.performed += Invisible;
        _movementInvisible.canceled += Visible;

        _movementDoor.started += Door;

        _movementTalk.started += Talk;

    }

    private void OnDisable()
    {
        controls.Player.Movement.Disable();

       // _movementJump.started -= Jump;
        _movementJump.performed -= HoldJump;
        _movementJump.canceled -= OffJump;
        _movementJump.Disable();

        _movementInvisible.performed -= Invisible;
        _movementInvisible.canceled -= Visible;
        _movementInvisible.Disable();

        _movementDoor.started -= Door;
        _movementDoor.Disable();

        _movementTalk.started -= Talk;
        _movementTalk.Disable();
    }

    private void HoldJump(InputAction.CallbackContext context) {

        if (canMove) { 

         if (isGrounded == true)
        {
            AudioManager.PlaySFX(AudioManager.SFXJump);
            Jumping = true;
            Player_Animator.SetBool("IsJumping", true);
            jumpTimeCounter = jumpTime;
            rb.velocity = (Vector2.up * JumpAmount);
        }

            if (Jumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = (Vector2.up * JumpAmount);
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    Jumping = false;
                }


            }
        }
    }

    private void OffJump(InputAction.CallbackContext context)
    {
            Jumping = false;

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Invisible(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            if (sceneName != "SCN_Level_1")
            {
                AudioManager.PlaySFX(AudioManager.SFXTransparent);
                Color col = sprite_renderer.color;
                col.a = 0.333f;
                sprite_renderer.color = col;
                Intensity.intensity = 0.15f;
               // Player_Animator.SetBool("Crouch", true);
                Physics2D.IgnoreLayerCollision(6, 7, true);
                movement_speed = 0f;
                JumpAmount = 0f;
                phantom = true;
                Ghost.isChasing = false;
                Ghost2.isChasing = false;
                Ghost3.isChasing = false;
            }
        }
    }

    private void Visible(InputAction.CallbackContext context)
    {
        Color col = sprite_renderer.color;
        col.a = 1;
        sprite_renderer.color = col;
        Intensity.intensity = 1f;
        // Player_Animator.SetBool("Crouch", false);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        movement_speed = 6f;
        JumpAmount = 10f;
        phantom = false;

}


    private void Door(InputAction.CallbackContext context)
    {

    }

    private void Talk(InputAction.CallbackContext context)
    {

    }


    // Movement
    void lateral()
    {
        Player_Animator.SetFloat("Speed", Mathf.Abs(moveInput));

       if (KBCounter <= 0)
      {
            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * movement_speed, rb.velocity.y);
            
   }

       else
    {
          if (KnockFromRight == true)
       {
              rb.velocity = new Vector2(-KBForce, KBForce);
        }
         else if (KnockFromRight == false)
         {
            rb.velocity = new Vector2(KBForce, KBForce);
         }
      }

        KBCounter -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D Ground_Detector)
    {
        Jumping = false;
        Player_Animator.SetBool("IsJumping", false);
        Debug.Log("Coucou");

        if (isGrounded == true)
        {
            AudioManager.PlaySFX(AudioManager.SFXLanding);
            ParticleController.fallParticle.Play();
        }
    }
    private void ExitTriggerEnter2D(Collider2D Ground_Detector)
    {
        Jumping = true;
    }

    public void stopMovement()
    {
        canMove = false;
        movement_speed = 0;
        rb.velocity = Vector2.zero;
        Player_Animator.SetFloat("Speed", 0);
    }

    public void playMovement()
    {
        canMove = true;
        movement_speed = 6f;
    }
}

