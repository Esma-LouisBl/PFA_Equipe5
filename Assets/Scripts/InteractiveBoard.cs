using UnityEngine;

public class InteractiveBoard : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;
    private float x;

    public Collider waypointArea;
    private bool isDragging = false;
    


    void Start()
    {
        x = transform.position.x;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = x;
        transform.position = pos;
    }

    void OnMouseDown()
    {
        isDragging = true;
        
        offset = transform.position - MouseWorldPosition();
        
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void OnMouseDrag()
    {
        
        Vector3 newPosition = MouseWorldPosition() + offset;
        newPosition = ClampToWaypointArea(newPosition);
        transform.position = newPosition;
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


    private void OnTriggerEnter(Collider other)
    {
        if (!isDragging && other.CompareTag("Selectable") && other.gameObject != gameObject)
        {
            if (other.transform.parent != transform)
            {
                other.transform.SetParent(transform, true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!isDragging && other.CompareTag("Selectable") && other.transform.parent == transform)
        {
            other.transform.SetParent(null, true);
        }
    }


}


