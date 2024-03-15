using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    [SerializeField] int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
