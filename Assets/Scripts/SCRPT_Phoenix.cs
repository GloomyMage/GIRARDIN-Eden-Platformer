using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SCRPT_Phoenix : MonoBehaviour
{
    // Other Scripts
    [Header("----------===== Other Scripts =====----------")]
    public Light2D Intensity;
    public SCRPT_AudioManager AudioManager;
    public NewControls controls;

    // Inputs
    [Header("----------=====  Inputs  =====----------")]
    private InputAction _movementAction;
    private InputAction _movementEscape;

    // Attributes
    [Header("----------=====  Attributes  =====----------")]
    [SerializeField] Animator Player_Animator;
    [SerializeField] SpriteRenderer sprite_renderer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] CapsuleCollider2D capsule_collider;
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
    public bool isGrounded;
    public Transform Ground_Detector;
    public float checkRadius;
    public LayerMask whatIsGround;

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
    }

    private void Awake()
    {
        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SCRPT_AudioManager>();
        controls = new NewControls();
        Physics2D.IgnoreLayerCollision(7, 9, true);
        Physics2D.IgnoreLayerCollision(9, 10, true);
    }



    void Update()
    {
        if (canMove)
        {
           /* lateral();*/
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(Ground_Detector.position, checkRadius, whatIsGround);

       // Player_Animator.SetFloat("Falling", (rb.velocity.y));

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

          //  Player_Animator.SetFloat("Speed", Mathf.Abs(moveInput));

         /*   if (KBCounter <= 0)
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
         */

        }

    }


            private void OnEnable()
    {
        _movementAction = controls.Player.Movement;
        _movementAction.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Movement.Disable();
    }


    // Movement
    void lateral()
    {
        // Player_Animator.SetFloat("Speed", Mathf.Abs(moveInput));

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

    public void stopMovement()
    {
        canMove = false;
        movement_speed = 0;
        rb.velocity = Vector2.zero;
       //  Player_Animator.SetFloat("Speed", 0);
    }

    public void playMovement()
    {
        canMove = true;
        movement_speed = 6f;
    }
}
