using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 20f;
    [SerializeField] private float rotationSpeed = 350f;
    [SerializeField] private float distance = 1.5f;
    [SerializeField] private CursorController cursorController;
    [SerializeField] private LayerMask waypointLayer;

    private Vector3 waypointPosition;
    private Quaternion targetRotation;

    private bool isMoving = false;
    public bool isRotating = false;

    void Update()
    {
        if (GameManager.Instance.PlayerCanMove)
        {
            if (!isMoving && !isRotating)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    targetRotation = Quaternion.Euler(0f, transform.eulerAngles.y - 90f, 0f);
                    isRotating = true;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    targetRotation = Quaternion.Euler(0f, transform.eulerAngles.y + 90f, 0f);
                    isRotating = true;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    TryToMove(transform.forward);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    TryToMove(-transform.forward);
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
        cursorController.EnableCursor(false);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            transform.rotation = targetRotation;

            cursorController.EnableCursor(true);

            isRotating = false;
        }
    }
}