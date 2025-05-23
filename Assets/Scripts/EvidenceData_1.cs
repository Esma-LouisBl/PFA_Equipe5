using UnityEngine;

[CreateAssetMenu(fileName = "Evidence", menuName = "Folders/Evidence")]
public class EvidenceData : ScriptableObject
{
    public string Name;
    public Sprite EvidenceSprite;
    public GameObject EvidenceGO;
    public string Informations;

    public StoryScene ReactionPeter;
    public StoryScene ReactionHolly;
    public StoryScene ReactionOliver;
    public StoryScene ReactionInspector;
}
