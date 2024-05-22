using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_MengPo : SCRPT_Interaction, SCRPT_Talk
{
    [SerializeField] private Conversation conversation;

    public void Convo(Conversation conversation)
    {
      
    }

    public override void Interact()
    {
        Convo(conversation);
    }

}
