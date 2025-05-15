using UnityEngine;

[CreateAssetMenu(fileName = "EvidenceData", menuName = "Folders/EvidenceData")]
public class EvidenceData : ScriptableObject
{
    public string Name;
    public GameObject Mesh;
    public string Description;
    public string SuspectConcerned;
}
