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

    private AudioClip audioClipChoice;

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
        _gameController.currentScene = contactList[_index].scene;
        _gameController.restart = true;
        _cursorController.LookForward();
        _index = 0;
        OpenAndClose();
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
        //RandomAudioClip();
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

        //RandomAudioClip();
    }

    public void OpenAndClose()
    {
        if (_window.activeSelf)
        {
            _window.SetActive(false);
            _gameManager.readingNote = false;
            //PlayASound(AudioManager.Instance._pickUp);
        }
        else
        {
            if (_gameManager.playerCanMove)
            {
                _window.SetActive(true);
                _gameManager.readingNote = true;
                //PlayASound(AudioManager.Instance._hangUp);
            }
        }
    }

    public void PlayASound(AudioClip audioClip)
    {
        AudioManager.Instance.PlaySFX(audioClip);
    }
    //public void RandomAudioClip()
    //{
    //    AudioClip[] audioClips = new AudioClip[3] { AudioManager.Instance._Phone01, AudioManager.Instance._Phone02, AudioManager.Instance._Phone03 };
    //    audioClipChoice = audioClips[Random.Range(0, audioClips.Length)];
        //PlayASound(audioClipChoice);
    //}
}
