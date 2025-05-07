using UnityEngine;

[CreateAssetMenu(fileName = "NewEvidence", menuName = "Data/New Evidence")]
public class Evidence : ScriptableObject
{
    public string evidenceName, evidenceDescription;
    public Mesh evidenceMesh;
    public Material evidenceMaterial;
}
