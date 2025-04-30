using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] 
    private Texture2D _customCursor;

    [SerializeField] 
    private Transform _cameraPivot;

    [SerializeField] 
    public float _speed = 8f;
    private float maxAngleX = 10f;
    private float maxAngleY = 10f;

    private Vector2 screenCenter;

    private bool playerCanUseCursor = true;

    public void EnableCursor(bool condition)
    {
        playerCanUseCursor = condition;

        if (condition)
        {
            Cursor.visible = true;
            Cursor.SetCursor(_customCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.visible = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            _cameraPivot.localRotation = Quaternion.identity;
        }
    }

    void Start()
    {
        Cursor.SetCursor(_customCursor, screenCenter, CursorMode.Auto);

        screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (GameManager.Instance.PlayerCanMove)
        {
            if (playerCanUseCursor)
            {
                Vector2 mousePos = Input.mousePosition;
                Vector2 offset = mousePos - screenCenter;

                float normalizedX = Mathf.Clamp(offset.x / screenCenter.x, -1f, 1f);
                float normalizedY = Mathf.Clamp(offset.y / screenCenter.y, -1f, 1f);

                float angleX = -normalizedY * maxAngleX;
                float angleY = normalizedX * maxAngleY;

                Quaternion targetRotation = Quaternion.Euler(angleX, angleY, 0f);
                _cameraPivot.localRotation = Quaternion.Slerp(_cameraPivot.localRotation, targetRotation, Time.deltaTime * _speed);
            }
        }
    }
}