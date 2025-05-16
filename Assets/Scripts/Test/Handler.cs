using UnityEngine;
using UnityEngine.UI;

public class Handler : MonoBehaviour
{
    [SerializeField]
    private Transform _playerHandPosition;
    
    public GameObject CurrentEvidence;

    [SerializeField]
    private Image _evidence;
    [SerializeField]
    private Sprite _emptyImage;

    public void HoldEvidence(EvidenceData evidence)
    {
        //if (!GameManager.Instance.PlayerHasEvidence)
        //{
            CurrentEvidence = evidence.EvidenceGO;
            _evidence.sprite = evidence.EvidenceSprite;
            GameManager.Instance.PlayerHasEvidence = true;
        //}
    }

    public void DropEvidence()
    {
        _evidence.sprite = null;
        GameManager.Instance.PlayerHasEvidence = false;
    }
}
