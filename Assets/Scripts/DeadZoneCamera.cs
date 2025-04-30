using UnityEngine;
using UnityEngine.EventSystems;

public class DeadZoneCamera : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private CursorController cursorController;

    private float _baseSpeed;

    private void Awake()
    {
        _baseSpeed = cursorController._speed;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cursorController._speed = _baseSpeed;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cursorController._speed = 3f;
    }
}
