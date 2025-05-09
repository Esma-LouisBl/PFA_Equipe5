using UnityEngine;
public class InteractiveBoard : MonoBehaviour
{
    private Vector3 offset;
    private float x;
    [SerializeField]
    private Collider waypointArea;
    private bool isDragging = false;
    private int dragButton = 0; // 0 = left, 1 = right

    void Start()
    {
        x = transform.position.x;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = x;
        transform.position = pos;

        HandleRightClickDrag();
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragButton = 0;
            offset = transform.position - MouseWorldPosition();
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void OnMouseDrag()
    {
        if (dragButton == 0 && isDragging)
        {
            Vector3 newPosition = MouseWorldPosition() + offset;
            newPosition = ClampToWaypointArea(newPosition);
            transform.position = newPosition;
        }
    }

    void HandleRightClickDrag()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // Raycast to check if we're clicking on this object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider == GetComponent<Collider>())
            {
                isDragging = true;
                dragButton = 1;
                offset = transform.position - MouseWorldPosition();
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        if (isDragging && dragButton == 1 && Input.GetMouseButton(1))
        {
            Vector3 newPosition = MouseWorldPosition() + offset;
            newPosition = ClampToWaypointArea(newPosition);
            transform.position = newPosition;
        }
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    Vector3 ClampToWaypointArea(Vector3 position)
    {
        Bounds bounds = waypointArea.bounds;
        position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
        position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);
        position.z = Mathf.Clamp(position.z, bounds.min.z, bounds.max.z);
        return position;
    }




    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!isDragging && other.CompareTag("Selectable") && other.gameObject != gameObject)
    //    {
    //        if (other.transform.parent != transform)
    //        {
    //            other.transform.SetParent(transform, true);
    //        }
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (!isDragging && other.CompareTag("Selectable") && other.transform.parent == transform)
    //    {
    //        other.transform.SetParent(null, true);
    //    }
    //}


}


