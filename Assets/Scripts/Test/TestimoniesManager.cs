using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class TestimoniesManager : MonoBehaviour
{
    private List<TestimonyData> _unlockedTestimonies = new List<TestimonyData>();
    private int _currentIndex;

    [SerializeField]
    private Image _leftArrow;
    [SerializeField]
    private Image _rightArrow;
    [SerializeField]
    private Image _testimonyLeft;
    [SerializeField]
    private Image _testimonyRight;

    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private TextMeshProUGUI _description;

    [SerializeField]
    private GameObject _window;

    public void UnlockedTestimony(TestimonyData newTestimony)
    {
        if (!_unlockedTestimonies.Contains(newTestimony))    //check if the testimony has already been collected
        {
            _unlockedTestimonies.Add(newTestimony);
            UIUpdate();
        }
    }

    private void UIUpdate()
    {
        _name.text = _unlockedTestimonies[_currentIndex].Name;
        _description.text = _unlockedTestimonies[_currentIndex].Description;

        bool canUseLeft = _currentIndex > 0;
        bool canUseRight = _currentIndex < _unlockedTestimonies.Count - 1;

        _leftArrow.gameObject.SetActive(canUseLeft);
        _testimonyLeft.gameObject.SetActive(canUseLeft);

        _rightArrow.gameObject.SetActive(canUseRight);
        _testimonyRight.gameObject.SetActive(canUseRight);
    }

    public void NextTestimony()
    {
        if (_currentIndex < _unlockedTestimonies.Count - 1)
        {
            _currentIndex++;
        }
        else
        {
            _currentIndex = 0;
        }
        UIUpdate();
    }
    
    public void PreviousTestimony()
    {
        if (_currentIndex > 0)
        {
            _currentIndex--;
        }
        else
        {
            _currentIndex = _unlockedTestimonies.Count - 1;
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
