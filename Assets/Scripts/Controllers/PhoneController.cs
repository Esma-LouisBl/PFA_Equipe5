using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneController : MonoBehaviour
{
    public List<PhoneContact> contactList;
    private int _index;

    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private GameController _gameController;
    [SerializeField]
    private GameObject _window;
    [SerializeField]
    private MeshOutliner _outliner;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private CursorController _cursorController;

    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _pickup, _hangup, _change1, _change2, _change3;

    void Update()
    {
        _name.text = contactList[_index].contactName;

        if (_outliner.selectedPhone == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                OpenAndClose();
            }
        }
    }

    public void LoadScene()
    {
        OpenAndClose();
        _gameController.currentScene = contactList[_index].scene;
        _gameController.restart = true;
        _cursorController.LookForward();
        _index = 0;
    }

    public void ChangeIndexUp()
    {
        if (_index < contactList.Count -1)
        {
            _index++;
        }
        else
        {
            _index = 0;
        }
        UseASound();
        _audioSource.Play();
    }

    public void ChangeIndexDown()
    {
        if (_index > 0)
        {
            _index--;
        }
        else
        {
            _index = contactList.Count -1;
        }
        UseASound();
        _audioSource.Play();
    }

    public void OpenAndClose()
    {
        if (_window.activeSelf)
        {
            _window.SetActive(false);
            _gameManager.readingNote = false;
            _audioSource.clip = _pickup;
            _audioSource.Play();
        }
        else
        {
            if (_gameManager.playerCanMove)
            {
                _window.SetActive(true);
                _gameManager.readingNote = true;
                _audioSource.clip = _hangup;
                _audioSource.Play();
            }
        }
    }

    private void UseASound()
    {
        int random = Random.Range(0, 3);
        if (random == 0)
        {
            _audioSource.clip = _change1;
        }
        if (random == 1)
        {
            _audioSource.clip = _change2;
        }
        if (random == 2)
        {
            _audioSource.clip = _change3;
        }
    }
}
