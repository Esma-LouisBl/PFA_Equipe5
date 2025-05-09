using UnityEngine;
using UnityEngine.EventSystems;

public class MeshOutliner : MonoBehaviour
{
    private Transform _highlight;
    private RaycastHit raycastHit;

    public string item;
    public bool selected = false;

    void Update()
    {
        if (_highlight != null)
        {
            _highlight.gameObject.GetComponent<Outline>().enabled = false;
            _highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit))
        {
            _highlight = raycastHit.transform;

            if (_highlight.CompareTag("Selectable") && _highlight.gameObject.name == gameObject.name)
            {
                if (_highlight.gameObject.GetComponent<Outline>() != null)
                {
                    _highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = _highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    _highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.white;
                    _highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }

                selected = true;
            }
            else
            {
                _highlight = null;
                selected = false;
            }
        }
    }
}