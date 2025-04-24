using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementSpeed = 20f;
    private float rotationSpeed = 350f;
    [SerializeField]
    private float distance = 1.5f;
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private LayerMask waypointLayer;
    private Vector3 waypointPosition;
    private Quaternion targetRotation;

    private bool isMoving = false;
    private bool isRotating = false;
    private bool playerCanMove = true; // Test
    void Update()
    {
        if (playerCanMove)
        {
            if (!isMoving && !isRotating)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    targetRotation = Quaternion.Euler(0f, playerTransform.eulerAngles.y - 90f, 0f);
                    isRotating = true;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    targetRotation = Quaternion.Euler(0f, playerTransform.eulerAngles.y + 90f, 0f);
                    isRotating = true;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    TryToMove(playerTransform.forward);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    TryToMove(-playerTransform.forward);
                }
            }

            if (isMoving)
            {
                Move();
            }

            if (isRotating)
            {
                Rotation();
            }
        }
    }
    private void TryToMove(Vector3 direction)
    {
        Ray ray = new Ray(transform.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, distance, waypointLayer))
        {
            waypointPosition = hit.transform.position;
            isMoving = true;
        }
    }
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypointPosition, movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, waypointPosition) < 0.01f)
        {
            transform.position = waypointPosition;
            isMoving = false;
        }
    }

    private void Rotation()
    {
        playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(playerTransform.rotation, targetRotation) < 1f)
        {
            playerTransform.rotation = targetRotation;
            isRotating = false;
        }
    }
}