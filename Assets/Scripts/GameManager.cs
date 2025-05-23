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

    public bool inspectorAble;

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
}
