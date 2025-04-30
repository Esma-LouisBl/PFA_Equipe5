using UnityEngine;
using UnityEngine.EventSystems;

public class DeadZoneCamera : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private CursorController cursorController;

    private float _baseSpeed;
    [SerializeField] private float _highSpeed;

    private void Awake()
    {
        _baseSpeed = cursorController._speed;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cursorController._speed = _highSpeed;
    }
}
