using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SuspectsController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _alibiPeter, _alibiHolly, _alibiOliver;
    [SerializeField]
    private GameObject _window, _peter, _holly, _oliver;

    private int _suspectIndex;

    [SerializeField]
    private MeshOutliner _outliner;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();

        _alibiPeter.enabled = false;
        _alibiHolly.enabled = false;
        _alibiOliver.enabled = false;

    }
    public void TurnOnAlibi(string suspect) //Show the alibi if the name of the person is written in the Alibi section of the sentence
    {
        if (suspect == "Peter")
        {
            _alibiPeter.enabled = true;
        }
        if (suspect == "Holly")
        {
            _alibiHolly.enabled = true;
        }
        if (suspect == "Oliver")
        {
            _alibiOliver.enabled = true;
        }
    }
    private void Update()
    {
        if (_outliner.item == "Suspects" && _outliner.selected)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                OpenAndClose();
            }
        }

        if (_suspectIndex == 0)
        {
            _peter.SetActive(true);
            _oliver.SetActive(false);
            _holly.SetActive(false);
        }

        if (_suspectIndex == 1)
        {
            _peter.SetActive(false);
            _oliver.SetActive(true);
            _holly.SetActive(false);
        }

        if (_suspectIndex == 2)
        {
            _peter.SetActive(false);
            _oliver.SetActive(false);
            _holly.SetActive(true);
        }
    }
    public void ChangeSuspectUp()
    {
        if (_suspectIndex < 2)
        {
            _suspectIndex++;
        }
        else
        {
            _suspectIndex = 0;
        }
    }

    public void ChangeSuspectDown()
    {
        if (_suspectIndex > 0)
        {
            _suspectIndex--;
        }
        else
        {
            _suspectIndex = 2;
        }
    }

    public void OpenAndClose()
    {
        if (_window.activeSelf)
        {
            _window.SetActive(false);
            _gameManager.playerCanMove = true;
        }
        else
        {
            _window.SetActive(true);
            _gameManager.playerCanMove = false;
        }
    }
}
