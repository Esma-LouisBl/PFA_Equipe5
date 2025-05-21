using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestimoniesManager : MonoBehaviour
{
    [SerializeField]
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
    [SerializeField]
    private MeshOutliner _outliner;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private GameObject _returnCanvas;
    [SerializeField]
    private AudioSource _audioSource;

    private void Update()
    {
        if (_outliner.selectedTestimonies == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                OpenAndClose();
            }
        }
    }
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
        _audioSource.Play();
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
        _audioSource.Play();
    }

    public void OpenAndClose()
    {
        if (_window.activeSelf)
        {
            _window.SetActive(false);
            _gameManager.readingNote = false;
            _returnCanvas.SetActive(true);
        }
        else
        {
            if (!_gameManager.readingNote)
            {
                _window.SetActive(true);
                _gameManager.readingNote = true;
                _returnCanvas.SetActive(false);
            }
        }
    }
}
