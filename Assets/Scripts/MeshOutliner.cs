using UnityEngine;
using UnityEngine.EventSystems;

public class MeshOutliner : MonoBehaviour
{
    private Transform _highlight;
    private RaycastHit raycastHit;

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

            if (_highlight.CompareTag("Selectable"))
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
            }
            else
            {
                _highlight = null;
            }
        }
    }
}