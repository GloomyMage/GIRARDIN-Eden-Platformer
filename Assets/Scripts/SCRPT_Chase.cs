using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_Chase : MonoBehaviour
{
    public SCRPT_Player_Movement player;
    public SCRPT_AudioManager AudioManager;

    public SpriteRenderer sprite_renderer;
    [SerializeField] Animator Oni_Animator;
    public Transform[] patrolPoints;
    public int patrolDestination = 0;
    public float movespeed;
    public float walkspeed = 4f;
    public float chasespeed = 8f;
    public Transform playerTransform;
    public bool isChasing = false;
    public float chaseDistance = 3f;

    void Update()
    {
        enemymove();
    }

    private void enemymove()
    {
        if (isChasing)
        {
            Oni_Animator.SetBool("Wrath", true);

            if  (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance * 3 || player.phantom == true)
                {
                isChasing = false;

            }
            else if (transform.position.x > playerTransform.position.x)
            {
                movespeed = chasespeed;
                sprite_renderer.flipX = false;
                transform.position += Vector3.left * movespeed * Time.deltaTime;
            }

            else if (transform.position.x < playerTransform.position.x)
            {
                movespeed = chasespeed;
                sprite_renderer.flipX = true;
                transform.position += Vector3.right * movespeed * Time.deltaTime;
            }
        }
        else
        {
            Oni_Animator.SetBool("Wrath", false);

            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance && player.phantom == false)
            {
                AudioManager.PlaySFX(AudioManager.SFXEnemy);
                isChasing = true;

            }

                else if (patrolDestination == 0)
                {
                    sprite_renderer.flipX = true;
                    movespeed = walkspeed;

                    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, movespeed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, patrolPoints[0].position) < 2f)
                    {

                        patrolDestination = 1;
                    }
                }

                else if (patrolDestination == 1)
                {
                    sprite_renderer.flipX = false;
                    movespeed = walkspeed;

                    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, movespeed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, patrolPoints[1].position) < 2f)
                    {

                        patrolDestination = 0;
                    }
                }
        }
    }
}
