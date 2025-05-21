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

    public bool PlayerHasEvidence = false;

    //public Texture2D mainCursor, interactCursor, dialogueCursor;
    //public CursorMode cursorMode = CursorMode.Auto;
    //public Vector2 hotspot = Vector2.zero;

    [SerializeField]
    private GameObject _inventoryWindow;

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
            _inventoryWindow.SetActive(true);
        }
        else
        {
            playerCanMove = false;
            _inventoryWindow.SetActive(false);
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
