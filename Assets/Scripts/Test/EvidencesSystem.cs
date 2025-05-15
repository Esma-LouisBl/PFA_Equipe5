using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EvidencesSystem : MonoBehaviour
{
    [SerializeField] private List<EvidenceData> _allEvidences;
    private int currentIndex = 0;
    private EvidenceData currentEvidence;

    [SerializeField]
    private Handler _playerHandler;
    [SerializeField]
    private TextMeshProUGUI _testText;

    public void ChangeEvidence()
    {
        currentIndex++;
        currentEvidence = _allEvidences[currentIndex];
        _testText.text = currentEvidence.Name;

    }
    public void ChooseEvidence()
    {
        _playerHandler.HoldEvidence(currentEvidence);
        Debug.Log(GameManager.Instance.PlayerHasEvidence);
    }
}
