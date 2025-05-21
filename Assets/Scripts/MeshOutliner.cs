using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MeshOutliner : MonoBehaviour
{
    private Transform _highlight;
    private RaycastHit raycastHit;

    public string item;
    public bool selectedSuspects, selectedEvidences, selectedTestimonies, selectedPhone, selectedDesk, selectedCabinet;

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

                MeshOutliner meshOutliner = _highlight?.gameObject?.GetComponent<MeshOutliner>();   //check what object is highlighted
                
            if (meshOutliner != null)
                {
                    if (meshOutliner.item == "Evidences")
                    {
                        selectedEvidences = true;
                    }
                    if (meshOutliner.item == "Suspects")
                    {
                        selectedSuspects = true;
                    }
                    if (meshOutliner.item == "Testimonies")
                    {
                        selectedTestimonies = true;
                    }
                    if (meshOutliner.item == "Phone")
                    {
                        selectedPhone = true;
                    }
                    if (meshOutliner.item == "Desk")
                    {
                        selectedDesk = true;
                    }
                    if (meshOutliner.item == "Cabinet")
                    {
                        selectedCabinet = true;
                    }

                }
            }
            else
            {
                selectedEvidences = false;
                selectedSuspects = false;
                selectedTestimonies = false;
                selectedPhone = false;
                selectedDesk = false;
                selectedCabinet = false;
            }
        if (selectedCabinet && selectedDesk)
        {
            selectedCabinet = false;
            selectedDesk = true;
        }
    }
}