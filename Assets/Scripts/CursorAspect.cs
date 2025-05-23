using UnityEngine;

public class CursorAspect : MonoBehaviour
{
    public Texture2D mainCursor, interactCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotspot = Vector2.zero;


    private void OnMouseEnter()
    {
        Cursor.SetCursor(interactCursor, hotspot, cursorMode);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(mainCursor, Vector2.zero, cursorMode);
    }
}
