using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_PatrolBorder : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int patrolDestination = 0;
    [SerializeField] float movespeed = 1f;

    // Update is called once per frame
    void Update()
    {
        if (patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, movespeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f);
            {
               // sprite_renderer.flipX = false;
                patrolDestination = 1;
            }
        }

        if (patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, movespeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f);
            {
               // sprite_renderer.flipX = true;
                patrolDestination = 0;
            }
        }
    }
}
