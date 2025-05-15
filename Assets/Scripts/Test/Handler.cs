using UnityEngine;

public class Handler : MonoBehaviour
{
    [SerializeField]
    private Transform _playerHandPosition;
    private GameObject currentEvidence;

    public bool playerHasEvidence;

    public void HoldEvidence(EvidenceData evidence)
    {
        currentEvidence = Instantiate(evidence.Mesh, _playerHandPosition);
        currentEvidence.transform.localPosition = Vector3.zero;
        currentEvidence.transform.localRotation = Quaternion.identity;
        playerHasEvidence = true;
    }

    public void DropEvidence()
    {
        Destroy(currentEvidence);
        playerHasEvidence = false;
    }
}
