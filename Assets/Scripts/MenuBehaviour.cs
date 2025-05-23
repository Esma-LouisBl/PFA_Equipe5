using UnityEngine;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _optionsButton;
    [SerializeField]
    private Button _bonusButton;
    [SerializeField]
    private Button _quitButton;


    private bool menuActivated;

    void Update()
    {
        if (menuActivated)
        {
            _startButton.interactable = false;
            _optionsButton.interactable = false;
            _bonusButton.interactable = false;
            _quitButton.interactable = false;
        }
        else
        {
            _startButton.interactable = true;
            _optionsButton.interactable = true;
            _bonusButton.interactable = true;
            _quitButton.interactable = true;
        }
    }

    public void MenuActivated()
    {
        menuActivated = true;
    }
    public void MenuDeactivated()
    {
        menuActivated = false;
    }
}
