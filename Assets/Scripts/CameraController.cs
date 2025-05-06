using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CameraController : MonoBehaviour
{
    private Transform playerCamera;
    private float xRotation = 0f;

    public float sensitivity = 2f;


    void Start()
    {
        playerCamera = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Rotation de la caméra
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}