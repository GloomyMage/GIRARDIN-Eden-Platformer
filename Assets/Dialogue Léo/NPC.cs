using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.InputSystem;

public abstract class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer InteractSprite;
    private Transform PlayerTransform;
    private const float interactDistance = 2.5f;
    


    private void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
     
    }

    private void Update()
    {
            
     
        if (Input.GetButtonDown("DialogueCustom") && IsWithinInteractDistance())
        {
            //-------------------Intéraction with a NPC--------------
            Interact();
        }
        if (InteractSprite.gameObject.activeSelf && !IsWithinInteractDistance())
        {
            // ----------------set interraction sprite OFF ----------------
            InteractSprite.gameObject.SetActive(false);
            
          

        }
        else if (!InteractSprite.gameObject.activeSelf && IsWithinInteractDistance())
        {
            //----------- set interraction sprite ON ------------

            InteractSprite.gameObject.SetActive(true);
        }


    }
    
    public abstract void Interact();


    // ----------------check distance between the player and the NPC-------------
    private bool IsWithinInteractDistance()
    {

        if (Vector2.Distance(PlayerTransform.position, transform.position) < interactDistance)
        {
            return true;
        }
        else
        {
            return false;
            

        }


    }

}
