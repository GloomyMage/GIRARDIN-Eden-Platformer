using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_Chase : MonoBehaviour
{
    public SCRPT_Player_Movement player;

    public SpriteRenderer sprite_renderer;
    public Transform[] patrolPoints;
    public int patrolDestination = 0;
    [SerializeField] float movespeed = 3f;
    public Transform playerTransform;
    public bool isChasing = false;
    public float chaseDistance = 5f;

    void Update()
    {
        enemymove();
    }

    private void enemymove()
    {
        if (isChasing)
        {
            if  (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance * 5 || player.phantom == true)
                {
                isChasing = false;
            }
            if (transform.position.x > playerTransform.position.x)
            {
                sprite_renderer.flipX = false;
                transform.position += Vector3.left * movespeed * Time.deltaTime;
            }

            if (transform.position.x < playerTransform.position.x)
            {
                sprite_renderer.flipX = true;
                transform.position += Vector3.right * movespeed * Time.deltaTime;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance && player.phantom == false)
            {
                isChasing = true;
            }

                else if (patrolDestination == 0)
                {
                    sprite_renderer.flipX = true;

                    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, movespeed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, patrolPoints[0].position) < 2f)
                    {

                        patrolDestination = 1;
                    }
                }

                else if (patrolDestination == 1)
                {
                    sprite_renderer.flipX = false;

                    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, movespeed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, patrolPoints[1].position) < 2f)
                    {

                        patrolDestination = 0;
                    }
                }
        }
    }
}
