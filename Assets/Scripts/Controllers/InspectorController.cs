using UnityEngine;

public class InspectorController : MonoBehaviour
{
    public SpriteRenderer inspectorSprite;

    private void Start()
    {
        HideInspector();
    }

    public void ShowInspector()
    {
        inspectorSprite.enabled = true;
    }

    public void HideInspector()
    {
        inspectorSprite.enabled = false;
    }
}
