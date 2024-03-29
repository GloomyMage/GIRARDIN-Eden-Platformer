using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_Chase : MonoBehaviour
{
    public SpriteRenderer sprite_renderer;
    public Transform[] patrolPoints;
    public int patrolDestination = 0;
    [SerializeField] float movespeed = 5f;
    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;

    // Update is called once per frame
    void Update()
    {
        enemymove();
    }

    private void enemymove()
    {
        if (isChasing)
        {
            if (transform.position.x > playerTransform.position.x)
            {
                transform.position += Vector3.left * movespeed * Time.deltaTime;
            }

            if (transform.position.x < playerTransform.position.x)
            {
                transform.position += Vector3.right * movespeed * Time.deltaTime;
            }
        }

        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }

            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, movespeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
                {
                    Debug.Log("Hi");
                    sprite_renderer.flipX = false;
                    patrolDestination = 1;
                }
            }

            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, movespeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f)
                {
                    sprite_renderer.flipX = true;
                    patrolDestination = 0;
                }
            }
        }
    }
}
