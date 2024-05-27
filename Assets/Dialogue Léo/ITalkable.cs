using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITalkable 
{
    //Make the NPC able to have a dialogue
    public void Talk(DialogueText dialogueText);
}
