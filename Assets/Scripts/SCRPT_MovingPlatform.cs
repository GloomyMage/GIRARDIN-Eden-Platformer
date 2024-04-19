using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_MovingPlatform : MonoBehaviour
{
    public SpriteRenderer sprite_renderer;
    public Transform[] patrolPoints;
    public int patrolDestination = 0;
    [SerializeField] float movespeed = 3f;

    void Update()
    {
        platform();
    }

    private void platform()
    {
        if (patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, movespeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
            {
                sprite_renderer.flipX = true;
                patrolDestination = 1;
            }
        }

        if (patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, movespeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f)
            {
                sprite_renderer.flipX = false;
                patrolDestination = 0;
            }
        }
    }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.transform.parent = this.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.transform.parent = null;
            }
        }
 

}
