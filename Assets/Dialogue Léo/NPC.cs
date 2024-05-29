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
    public DialogueController Skip;
    public SCRPT_AudioManager AudioManager;



    private void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
     
    }

    private void Update()
    {
            
     
        if (Input.GetButtonDown("Chat") && IsWithinInteractDistance())
        {
            //-------------------Interaction with a NPC--------------
            Interact();
            AudioManager.PlaySFX(AudioManager.SFXButton);

        }
            if (InteractSprite.gameObject.activeSelf && !IsWithinInteractDistance())
        {
            // ----------------set interraction sprite OFF ----------------
            InteractSprite.gameObject.SetActive(false);
            
          

        }
        else if (!InteractSprite.gameObject.activeSelf && IsWithinInteractDistance())
        {
            //----------- set Interaction sprite ON ------------

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
