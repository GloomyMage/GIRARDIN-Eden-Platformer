using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Dialogue/New Dialogue Container")]
public class DialogueText : ScriptableObject
    // scriptable object that i can use to create personnalized dialogue for any NPC 
    // you can give them : a name, a sprite and as much dialogues boxes a you want
{
    public string NPCName;
   public Sprite NPCFace;

    [TextArea (5,10)]
    public string[] paragraphs;
}
