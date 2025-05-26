using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EvidencesSystem : MonoBehaviour
{
    [SerializeField] private List<EvidenceData> _allEvidences;
    private int currentIndex = 0;
    public EvidenceData currentEvidence;

    [SerializeField]
    private Handler _playerHandler;
    [SerializeField]
    private TextMeshProUGUI _testText;
    [SerializeField]
    private EvidencesController _evidencesController;

    public void Start()
    {
        currentEvidence = _allEvidences[currentIndex];
        _testText.text = currentEvidence.Name;
    }
    public void ChangeEvidenceUp()
    {
        if (currentIndex < _allEvidences.Count -1)
        {
            currentIndex++;
        }
        else
        {
            currentIndex = 0;
        }

        currentEvidence = _allEvidences[currentIndex];

    }
    public void ChangeEvidenceDown()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }
        else
        {
            currentIndex = _allEvidences.Count - 1;
        }

        currentEvidence = _allEvidences[currentIndex];
    }

        public void ChooseEvidence()
    {
        _playerHandler.HoldEvidence(currentEvidence);
        _evidencesController.OpenAndClose();
    }

    public void AddEvidence(EvidenceData evidence)
    {
        if (!_allEvidences.Contains(evidence))
        {
            _allEvidences.Add(evidence);
        }
    }

}
