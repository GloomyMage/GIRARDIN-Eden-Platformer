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
    public SCRPT_ParticleController ParticleController;
    public SCRPT_AudioManager AudioManager;
    public NewControls controls;

    // Inputs
    [Header("----------=====  Inputs  =====----------")]
    private InputAction _movementAction;
    private InputAction _movementJump;
    private InputAction _movementInvisible;
    private InputAction _movementDoor;

    // Attributes
    [Header ("----------=====  Attributes  =====----------")]
    [SerializeField] Animator Player_Animator;
    [SerializeField] SpriteRenderer sprite_renderer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] CapsuleCollider2D capsule_collider;
    public bool phantom = false;

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
        
        lateral();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(Ground_Detector.position, checkRadius, whatIsGround);

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


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

       // _movementJump.started += Jump;
        _movementJump.performed += HoldJump;
        _movementJump.canceled += OffJump;

        _movementInvisible.performed += Invisible;
        _movementInvisible.canceled += Visible;

        _movementDoor.started += Door;
        
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
    }

    //private void Jump(InputAction.CallbackContext context)
    //{

    //    if (isGrounded == true)
    //    {
    //        AudioManager.PlaySFX(AudioManager.SFXJump);
    //        Jumping = true;
    //        jumpTimeCounter = jumpTime;
    //        rb.velocity = (Vector2.up * JumpAmount);
    //    }

    //    if (Jumping == true)
    //    {
    //        if (jumpTimeCounter > 0)
    //        {
    //            rb.velocity = (Vector2.up * JumpAmount);
    //            jumpTimeCounter -= Time.deltaTime;
    //        }
    //        else
    //        {
    //            Jumping = false;
    //        }


    //    }

    //}

    private void HoldJump(InputAction.CallbackContext context) {

         if (isGrounded == true)
        {
            AudioManager.PlaySFX(AudioManager.SFXJump);
            Jumping = true;
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
        if (sceneName != "SCN_Level_1")
        {
            AudioManager.PlaySFX(AudioManager.SFXTransparent);
            Color col = sprite_renderer.color;
            col.a = 0.333f;
            sprite_renderer.color = col;
            Intensity.intensity = 0.15f;
            Ghost.isChasing = false;
            Physics2D.IgnoreLayerCollision(6, 7, true);
            movement_speed = 0f;
            JumpAmount = 0f;
            phantom = true;
        }
    }

    private void Visible(InputAction.CallbackContext context)
    {
        Color col = sprite_renderer.color;
        col.a = 1;
        sprite_renderer.color = col;
        Intensity.intensity = 1f;
        Physics2D.IgnoreLayerCollision(6, 7, false);
        movement_speed = 6f;
        JumpAmount = 10f;
        phantom = false;

}

    private void Door(InputAction.CallbackContext context)
    {

    }


    // Movement
    void lateral()
    {
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


    
        /*
        // Go left
        if (Input.GetKey(KeyCode.LeftArrow)) // While the left arrow is pressed
        {
            transform.Translate(Vector3.left * movement_speed * Time.deltaTime, Space.World); // Moves negatively on the X axis, therefore to the left
            Player_Animator.SetBool("BoolRun", true); // Run animation is triggered
            sprite_renderer.flipX = true; // Flips the Run animation on the X axis which is oriented to the right by default, therefore the animation is now oriented to the left



        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow)) // When the left arrow isn't pressed anymore
        {
            transform.Translate(Vector3.left * movement_speed * Time.deltaTime, Space.World); // Moves negatively on the X axis, therefore to the left
            Player_Animator.SetBool("BoolRun", true); // Run animation is triggered
            sprite_renderer.flipX = true; // Flips the Run animation on the X axis which is oriented to the right by default, therefore the animation is now oriented to the left
            Player_Animator.SetBool("BoolRun", false); // End of the run animation and go back to the idle animation
        }


        // Go right
        if (Input.GetKey(KeyCode.RightArrow)) // While the right arrow is pressed
        {
            transform.Translate(Vector3.right * movement_speed * Time.deltaTime, Space.World);  // Moves positively on the X axis, therefore to the right
            Player_Animator.SetBool("BoolRun", true); // Run animation is triggered
            sprite_renderer.flipX = false; // The run animation doesn't need to be flipped because it is already facing to the right

        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))  // When the right arrow isn't pressed anymore
        {
            transform.Translate(Vector3.right * movement_speed * Time.deltaTime, Space.World);  // Moves positively on the X axis, therefore to the right
            Player_Animator.SetBool("BoolRun", true); // Run animation is triggered
            sprite_renderer.flipX = false; // The run animation doesn't need to be flipped because it is already facing to the right
            Player_Animator.SetBool("BoolRun", false); // End of the run animation and go back to the idle animation
        }
        */

      


        // Jump

       


        


        /*
        if (Input.GetKeyDown(KeyCode.Space) && Jumping == false)
        {
            rb.AddForce(Vector2.up * JumpAmount, ForceMode2D.Impulse);
            Jumping = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Jumping == false && Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector2.up * JumpAmount, ForceMode2D.Impulse);
            Jumping = true;
            transform.Translate(Vector3.left * movement_speed * Time.deltaTime, Space.World); // Moves negatively on the X axis, therefore to the left
            Player_Animator.SetBool("BoolRun", true); // Run animation is triggered
            sprite_renderer.flipX = true; // Flips the Run animation on the X axis which is oriented to the right by default, therefore the animation is now oriented to the left
        }

        if (Input.GetKeyDown(KeyCode.Space) && Jumping == false && Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector2.up * JumpAmount, ForceMode2D.Impulse);
            Jumping = true;
            transform.Translate(Vector3.right * movement_speed * Time.deltaTime, Space.World);  // Moves positively on the X axis, therefore to the right
            Player_Animator.SetBool("BoolRun", true); // Run animation is triggered
            sprite_renderer.flipX = false; // The run animation doesn't need to be flipped because it is already facing to the right
        }
        */

        /*
        // VFX


        // TrailVFX
        */



    //public void Jump(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        rb.velocity = (Vector2.up * JumpAmount);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D SolDetection1)
    {
        Jumping = false;
        Debug.Log("Coucou");

        if (isGrounded == true)
        {
            AudioManager.PlaySFX(AudioManager.SFXLanding);
            ParticleController.fallParticle.Play();
        }
    }
    private void ExitTriggerEnter2D(Collider2D SolDetection1)
    {
        Jumping = true;
    }
}

