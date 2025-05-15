using UnityEngine;

[CreateAssetMenu(fileName = "Suspects", menuName = "Folders/Suspects")]
public class SuspectData : ScriptableObject
{
    public Sprite SuspectSprite;
    public string SuspectNumber;
    public string Informations;
    public string Alibi;
}
