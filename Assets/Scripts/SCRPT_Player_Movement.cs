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

    // Attributes
    [Header ("----------=====  Attributes  =====----------")]
    [SerializeField] Animator Player_Animator;
    [SerializeField] SpriteRenderer sprite_renderer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;

    // Knockback
    [Header("----------===== Knockback =====----------")]
    public float KBForce = 5;
    public float KBCounter = 0;
    public float KBTotalTime = 0.2f;
    public bool KnockFromRight;

    // Set speed movement speed
    [Header("----------===== Speed =====----------")]
    [SerializeField] float movement_speed = 1f;
    private float moveInput;

    // Jump
    [Header("----------===== Jump =====----------")]
    [SerializeField] float JumpAmount = 5;
    [SerializeField] bool Jumping = false;
    public bool isGrounded;
    public Transform Ground_Detector;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpTimeCounter;
    [SerializeField] float jumpTime = 0.33f;


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
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        lateral();
    }

    private void FixedUpdate()
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


    void movement()
    {
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

        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }


        // Jump

        isGrounded = Physics2D.OverlapCircle(Ground_Detector.position, checkRadius, whatIsGround);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.PlaySFX(AudioManager.SFXJump);
            Jumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = (Vector2.up * JumpAmount);
        }

        if (Input.GetKey(KeyCode.Space) && Jumping == true)
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

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Jumping = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Jumping = false;
        }



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

        // Transparent

        if (sceneName != "SCN_Level_1")
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                AudioManager.PlaySFX(AudioManager.SFXTransparent);
                Color col = sprite_renderer.color;
                col.a = 0.333f;
                sprite_renderer.color = col;
                Intensity.intensity = 0.15f;
                Ghost.isChasing = false;
                
            }
            else
            {
                Color col = sprite_renderer.color;
                col.a = 1;
                sprite_renderer.color = col;
                Intensity.intensity = 1f;
            }
        }

    }

    //public void Jump(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        rb.velocity = (Vector2.up * JumpAmount);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D SolDetection1)
    {
        AudioManager.PlaySFX(AudioManager.SFXLanding);
        ParticleController.fallParticle.Play();
        Jumping = false;
        Debug.Log("Coucou");
    }
    private void ExitTriggerEnter2D(Collider2D SolDetection1)
    {
        Jumping = true;
    }
}

