using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_Player_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    // Set speed movement speed
    [SerializeField] float movement_speed = 1f;
    [SerializeField] Animator Player_Animator;
    [SerializeField] SpriteRenderer sprite_renderer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float JumpAmount = 10;
    [SerializeField] bool Jumping = false;
    [SerializeField] GameObject GroundDetector;


    void movement()
    {
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


        // Jump
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

        // Transparent
        if (Input.GetKey(KeyCode.DownArrow))
        {
           Color col  = sprite_renderer.color;
            col.a = 0.25f;
            sprite_renderer.color = col;
        }
        else
        {
            Color col = sprite_renderer.color;
            col.a = 1;
            sprite_renderer.color = col;
        }

    }


    private void OnTriggerEnter2D(Collider2D SolDetection1)
    {
        Jumping = false;
        Debug.Log("Coucou");
    }
    private void ExitTriggerEnter2D(Collider2D SolDetection1)
    {
        Jumping = true;
    }
}

