using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private static bool _gameIsPaused;

    [SerializeField]
    private GameObject _pauseMenu;

    public void PauseMenuBehaviour()
    {
        if (_gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _gameIsPaused = true;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
