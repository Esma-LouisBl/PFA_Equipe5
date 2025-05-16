using System.Collections.Generic;
using UnityEngine;
public class RedLine : MonoBehaviour
{
    public Transform player;
    public LineRenderer rope;
    public LayerMask collMask;
    public GameObject ropePrefab;

    public List<Vector3> ropePositions { get; set; } = new List<Vector3>();

    private Vector3 offset;
    private float z;
    private bool isDragging = false;
    private GameObject currentRopeObject;

    private void Awake()
    {
        AddPosToRope(Vector3.zero);
        z = transform.position.z;
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        pos.z = z;
        transform.position = pos;

        if (Input.GetMouseButton(1))
        {
            if (isDragging)
            {
                Vector3 newPosition = MouseWorldPosition() + offset;
                transform.position = newPosition;
            }

            UpdateRopePositions();
            LastSegmentGoToPlayerPos();

            DetectCollisionEnter();
            if (ropePositions.Count > 2)
                DetectCollisionExits();
        }

        if (Input.GetMouseButtonDown(1))
        {
            
            if (currentRopeObject != null)
                Destroy(currentRopeObject);

            
            currentRopeObject = Instantiate(ropePrefab);
            rope = currentRopeObject.GetComponent<LineRenderer>();

            
            ropePositions = new List<Vector3>();

            
            AddPosToRope(player.position);

            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
            {
                isDragging = true;
                offset = transform.position - MouseWorldPosition();
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }
    }

    private void DetectCollisionEnter()
    {
        if (Physics.Linecast(player.position, rope.GetPosition(ropePositions.Count - 2), out RaycastHit hit, collMask))
        {
            ropePositions.RemoveAt(ropePositions.Count - 1);
            AddPosToRope(hit.point);
        }
    }

    private void DetectCollisionExits()
    {
        if (!Physics.Linecast(player.position, rope.GetPosition(ropePositions.Count - 3), out RaycastHit hit, collMask))
        {
            ropePositions.RemoveAt(ropePositions.Count - 2);
        }
    }

    private void AddPosToRope(Vector3 _pos)
    {
        ropePositions.Add(_pos);
        ropePositions.Add(player.position);
    }

    private void UpdateRopePositions()
    {
        rope.positionCount = ropePositions.Count;
        rope.SetPositions(ropePositions.ToArray());
    }

    private void LastSegmentGoToPlayerPos()
    {
        rope.SetPosition(rope.positionCount - 1, player.position);
    }

    private Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}

