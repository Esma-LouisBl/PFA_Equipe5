using UnityEngine;
using UnityEngine.EventSystems;

public class DeadZoneCamera : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private CursorController cursorController;

    [SerializeField] private float _highSpeed;


    public void OnPointerEnter(PointerEventData eventData)
    {
        cursorController._speed = _highSpeed;
    }
}