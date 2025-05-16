using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EvidencesSystem : MonoBehaviour
{
    [SerializeField] private List<EvidenceData> _allEvidences;
    private int currentIndex = 0;
    public EvidenceData currentEvidence;

    [SerializeField]
    private Handler _playerHandler;
    [SerializeField]
    private TextMeshProUGUI _testText;

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
            //_testText.text = currentEvidence.Name;
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
            //_testText.text = currentEvidence.Name;
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
    }
}
