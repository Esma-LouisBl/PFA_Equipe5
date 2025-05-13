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
    void Start()
    {
        
    }

    void Update()
    {
        _name.text = contactList[_index].name;

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
        Debug.Log(_index.ToString());
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
        Debug.Log(_index.ToString());

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
            _window.SetActive(true);
            _gameManager.readingNote = true;
        }
    }
}
