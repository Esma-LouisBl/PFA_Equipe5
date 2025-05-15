using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameController controller;

    public bool playerCanMove;
    public bool readingNote = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (controller.isActive == false && readingNote == false)
        {
            playerCanMove = true;
        }
        else
        {
            playerCanMove = false;
        }
    }

    public void GameOver()      //POUR LA DEMO (1er choix)
    {
        StartCoroutine(BlackScreen());
    }

    private IEnumerator BlackScreen()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0);
    }
}
