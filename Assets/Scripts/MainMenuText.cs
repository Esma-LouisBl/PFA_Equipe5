using UnityEngine;
using UnityEngine.UI;

public class MainMenuText : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    public void EnabledButton()
    {
        _button.interactable = true;
    }
}
