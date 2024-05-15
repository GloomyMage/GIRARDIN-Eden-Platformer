using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCRPT_PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    [SerializeField] int health;

    public SCRPT_GameController gameController;
    public SCRPT_Chase chase;

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
            gameController.Die();
            chase.isChasing = false;
            health = maxHealth;
           

            
        }
    }
}
