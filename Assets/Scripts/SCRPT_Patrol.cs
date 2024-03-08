using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_Patrol : MonoBehaviour
{
    [SerializeField] float enemy_speed = 5f;
    [SerializeField] float distance = 2f;
    private bool movingRight = true;
    public Transform ground_Detection;

  void Update()
    {
        transform.Translate(Vector2.right * enemy_speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(ground_Detection.position, Vector2.down, distance);
        if(groundInfo.collider == false)
        {
            if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}
