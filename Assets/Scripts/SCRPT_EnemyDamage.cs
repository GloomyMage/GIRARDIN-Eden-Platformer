using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_EnemyDamage : MonoBehaviour
{
    public int damage;
    public SCRPT_PlayerHealth playerHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
