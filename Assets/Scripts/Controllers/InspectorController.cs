using UnityEngine;

public class InspectorController : MonoBehaviour
{
    public SpriteRenderer inspectorSprite;
    public CursorAspect cursorAspect;
    public Texture2D mainCursor, dialogueCursor;

    private void Start()
    {
        HideInspector();
        cursorAspect.interactCursor = mainCursor;
    }

    public void ShowInspector()
    {
        inspectorSprite.enabled = true;
    }

    public void HideInspector()
    {
        inspectorSprite.enabled = false;
    }

    public void ChangeCursor()
    {
        if (cursorAspect.interactCursor == mainCursor)
        {
            cursorAspect.interactCursor = dialogueCursor;
        }
        if (cursorAspect.interactCursor == dialogueCursor)
        {
            cursorAspect.interactCursor = mainCursor;
        }
    }
}
