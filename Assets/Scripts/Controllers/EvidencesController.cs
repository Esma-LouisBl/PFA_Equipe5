using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EvidencesController : MonoBehaviour
{
    private int _evidenceIndex;

    [SerializeField]
    private GameObject _window;
    [SerializeField]
    private TextMeshProUGUI _name, _description;
    [SerializeField]
    private MeshFilter _mesh;

    public List<Evidence> evidences;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_window.activeSelf)
            {
                _window.SetActive(false);
            }
            else
            {
                _window.SetActive(true);
            }
        }

        _name.text = evidences[_evidenceIndex].name;
        _description.text = evidences[_evidenceIndex].evidenceDescription;
        _mesh.mesh = evidences[_evidenceIndex].evidenceMesh;
    }

    public void ChangeEvidenceUp()
    {
        if (_evidenceIndex < evidences.Count-1)
        {
            _evidenceIndex++;
        }
        else
        {
            _evidenceIndex = 0;
        }
    }

    public void ChangeEvidenceDown()
    {
        if (_evidenceIndex > 0)
        {
            _evidenceIndex--;
        }
        else
        {
            _evidenceIndex = evidences.Count-1;
        }
    }
}
