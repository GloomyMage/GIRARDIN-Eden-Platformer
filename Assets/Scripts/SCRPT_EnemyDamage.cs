using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_EnemyDamage : MonoBehaviour
{
    public int damage;
    public SCRPT_PlayerHealth playerHealth;
    public SCRPT_Player_Movement playerMovement;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            playerMovement.KBCounter = playerMovement.KBTotalTime;

            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.KnockFromRight = true;
            }
            if (collision.transform.position.x >= transform.position.x)
            {
                playerMovement.KnockFromRight =false;
            }

            playerHealth.TakeDamage(damage);
        }
    }
}
