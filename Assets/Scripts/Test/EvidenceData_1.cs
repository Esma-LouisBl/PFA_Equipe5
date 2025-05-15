using UnityEngine;

[CreateAssetMenu(fileName = "Evidence", menuName = "Folders/Evidence")]
public class EvidenceData : ScriptableObject
{
    public string Name;
    public GameObject MeshGO;
    public string Informations;

    public StoryScene ReactionPeter;
    public StoryScene ReactionHolly;
    public StoryScene ReactionOliver;
    public StoryScene ReactionInspector;
}
