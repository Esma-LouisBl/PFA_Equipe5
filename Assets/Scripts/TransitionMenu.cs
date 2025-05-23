using UnityEngine;

public class TransitionMenu : MonoBehaviour
{
    public GameObject WelcomeMenu;
    public GameObject MainMenu;


    public void ActivateMenu()
    {
        if (MainMenu != null)
        {
            MainMenu.SetActive(true);
            WelcomeMenu.SetActive(false);
        }
    }
}
