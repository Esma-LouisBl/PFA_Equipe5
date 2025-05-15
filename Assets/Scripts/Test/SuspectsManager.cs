using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class SuspectsManager : MonoBehaviour
{
    private List<SuspectData> unlockedSuspects = new List<SuspectData>();
    private int currentIndex;

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

    public void UnlockedEvidence(SuspectData newSuspect)
    {
        if (!unlockedSuspects.Contains(newSuspect))
        {
            unlockedSuspects.Add(newSuspect);
            UIUpdate();
        }
    }

    private void UIUpdate()
    {
        _suspectImage.sprite = unlockedSuspects[currentIndex].SuspectSprite;
        _number.text = unlockedSuspects[currentIndex].SuspectNumber;
        _description.text = unlockedSuspects[currentIndex].Informations;
        _alibi.text = unlockedSuspects[currentIndex].Alibi;

        bool canUseLeft = currentIndex > 0;
        bool canUseRight = currentIndex < unlockedSuspects.Count - 1;

        _leftArrow.gameObject.SetActive(canUseLeft);
        _suspectLeft.gameObject.SetActive(canUseLeft);

        _rightArrow.gameObject.SetActive(canUseRight);
        _suspectRight.gameObject.SetActive(canUseRight);
    }

    public void NextSuspect()
    {
        if (currentIndex < unlockedSuspects.Count - 1)
        {
            currentIndex++;
        }
        else
        {
            currentIndex = 0;
        }
        UIUpdate();
    }
    
    public void PreviousSuspect()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }
        else
        {
            currentIndex = unlockedSuspects.Count - 1;
        }
        UIUpdate();
    }

    public void OpenAndClose()
    {
        if (_window.activeSelf)
        {
            _window.SetActive(false);
            //_gameManager.readingNote = false;
        }
        else
        {
            _window.SetActive(true);
            //_gameManager.readingNote = true;
        }
    }
}
