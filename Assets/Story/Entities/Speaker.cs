using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewSpeaker", menuName = "Data/New Speaker")]
[System.Serializable]
public class Speaker : ScriptableObject
{
    public string speakerName;
    public Image image;
    public List<Sprite> sprites;
}
