using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameController controller;

    public bool playerCanMove;

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
        if (controller.isActive == false)
        {
            playerCanMove = true;
        }
        else
        {
            playerCanMove = false;
        }
    }
}
