using UnityEngine;

public class Handler : MonoBehaviour
{
    [SerializeField]
    private Transform _playerHandPosition;
    
    public GameObject CurrentEvidence;

    public void HoldEvidence(EvidenceData evidence)
    {
        if (!GameManager.Instance.PlayerHasEvidence)
        {
            CurrentEvidence = Instantiate(evidence.MeshGO, _playerHandPosition);
            CurrentEvidence.transform.localPosition = Vector3.zero;
            CurrentEvidence.transform.localRotation = Quaternion.identity;
            GameManager.Instance.PlayerHasEvidence = true;
        }
    }

    public void DropEvidence()
    {
        Destroy(CurrentEvidence);
        GameManager.Instance.PlayerHasEvidence = false;
    }
}
