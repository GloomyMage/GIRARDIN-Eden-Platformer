using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class David : NPC, ITalkable
{

    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;
   // [SerializeField] pushetpull canGrab;
    




    public override void Interact()
    {
       // if (canGrab.getIsGrabbing() == false)
        {
            Talk(dialogueText);
        }
        
        
    }


    public void Talk(DialogueText dialogueText)
    {
        
        // lancement de la conversation
        dialogueController.DisplayNextParagraph (dialogueText);
    }

}
