using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour
{
    [SerializeField]
    private Texture2D _customCursor;

    [SerializeField]
    private Transform _cameraPivot;

    [SerializeField]
    private float _maxSpeed = 8f;

    [SerializeField]
    private float deadZoneRadius = 50f;

    [SerializeField]
    private float maxAngleX = 10f, maxAngleY = 10f;

    private Vector2 screenCenter;
    private bool playerCanUseCursor = true;


    private bool _isRecentering = false;
    [SerializeField] private float _recenterDuration = 0.5f;

    public void EnableCursor(bool condition)
    {
        playerCanUseCursor = condition;

        if (condition)
        {
            Cursor.visible = true;
            //Cursor.SetCursor(_customCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.visible = false;
            //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            _cameraPivot.localRotation = Quaternion.identity;
        }
    }

    void Start()
    {
        screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        //Cursor.SetCursor(_customCursor, screenCenter, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (!GameManager.Instance.playerCanMove || !playerCanUseCursor || _isRecentering) return;

        Vector2 mousePos = Input.mousePosition;
        Vector2 offset = mousePos - screenCenter;

        float distance = offset.magnitude;

        if (distance < deadZoneRadius)      //Check if cursor in DeadZone
        {
            _cameraPivot.localRotation = Quaternion.Slerp(_cameraPivot.localRotation, Quaternion.identity, Time.deltaTime * _maxSpeed);
            return;
        }

        float normalizedDistance = Mathf.Clamp01((distance - deadZoneRadius) / (screenCenter.magnitude - deadZoneRadius));
        float currentSpeed = _maxSpeed * normalizedDistance;

        float normalizedX = Mathf.Clamp(offset.x / screenCenter.x, -1f, 1f);
        float normalizedY = Mathf.Clamp(offset.y / screenCenter.y, -1f, 1f);

        float angleX = -normalizedY * maxAngleX;
        float angleY = normalizedX * maxAngleY;

        Quaternion targetRotation = Quaternion.Euler(angleX, angleY, 0f);
        _cameraPivot.localRotation = Quaternion.Slerp(_cameraPivot.localRotation, targetRotation, Time.deltaTime * currentSpeed);
    }

    public void LookForward()   //called to look smoothly to the screen center
    {
        if (!_isRecentering)
            StartCoroutine(RecenterCameraCoroutine());
    }

    private IEnumerator RecenterCameraCoroutine()
    {
        _isRecentering = true;

        Quaternion startRotation = _cameraPivot.localRotation;
        Quaternion targetRotation = Quaternion.identity;

        float elapsed = 0f;
        while (elapsed < _recenterDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / _recenterDuration);
            _cameraPivot.localRotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        _cameraPivot.localRotation = targetRotation;
        _isRecentering = false;
    }
}
