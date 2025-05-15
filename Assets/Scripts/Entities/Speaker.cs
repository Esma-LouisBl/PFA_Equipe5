using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeaker", menuName = "Data/Entities/New Speaker")]
[System.Serializable]
public class Speaker : ScriptableObject
{
    public string speakerName;
    public Color textColor;
    public Sprite speakerSprite;
}
