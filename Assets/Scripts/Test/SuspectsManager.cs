using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class SuspectsManager : MonoBehaviour
{
    private List<SuspectData> _unlockedSuspects = new List<SuspectData>();
    private int _currentIndex;

    [SerializeField]
    private Image _leftArrow;
    [SerializeField]
    private Image _rightArrow;
    [SerializeField]
    private Image _suspectLeft;
    [SerializeField]
    private Image _suspectRight;

    [SerializeField]
    private Image _suspectImage;
    [SerializeField]
    private TextMeshProUGUI _number;
    [SerializeField]
    private TextMeshProUGUI _description;
    [SerializeField]
    private TextMeshProUGUI _alibi;

    [SerializeField]
    private GameObject _window;
    [SerializeField]
    private MeshOutliner _outliner;
    [SerializeField]
    private GameManager _gameManager;

    private void Update()
    {
        if (_outliner.selectedSuspects == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                OpenAndClose();
            }
        }
    }
    public void UnlockedEvidence(SuspectData newSuspect)
    {
        if (!_unlockedSuspects.Contains(newSuspect))
        {
            _unlockedSuspects.Add(newSuspect);
            UIUpdate();
        }
    }

    private void UIUpdate()
    {
        _suspectImage.sprite = _unlockedSuspects[_currentIndex].SuspectSprite;
        _number.text = _unlockedSuspects[_currentIndex].SuspectNumber;
        _description.text = _unlockedSuspects[_currentIndex].Informations;
        _alibi.text = _unlockedSuspects[_currentIndex].Alibi;

        bool canUseLeft = _currentIndex > 0;
        bool canUseRight = _currentIndex < _unlockedSuspects.Count - 1;

        _leftArrow.gameObject.SetActive(canUseLeft);
        _suspectLeft.gameObject.SetActive(canUseLeft);

        _rightArrow.gameObject.SetActive(canUseRight);
        _suspectRight.gameObject.SetActive(canUseRight);
    }

    public void NextSuspect()
    {
        if (_currentIndex < _unlockedSuspects.Count - 1)
        {
            _currentIndex++;
        }
        else
        {
            _currentIndex = 0;
        }
        UIUpdate();
    }
    
    public void PreviousSuspect()
    {
        if (_currentIndex > 0)
        {
            _currentIndex--;
        }
        else
        {
            _currentIndex = _unlockedSuspects.Count - 1;
        }
        UIUpdate();
    }

    public void OpenAndClose()
    {
        if (_window.activeSelf)
        {
            _window.SetActive(false);
            _gameManager.readingNote = false;
        }
        else
        {
            if (!_gameManager.readingNote)
            {
                _window.SetActive(true);
                _gameManager.readingNote = true;
            }
        }
    }
}
