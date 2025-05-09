using System.Collections;
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
    [SerializeField]
    private MeshRenderer _renderer;

    private float _rotation;

    public List<Evidence> evidences;

    [SerializeField]
    private MeshOutliner _outliner;

    [SerializeField]
    private GameManager _gameManager;

    private void Start()
    {
        StartCoroutine(EvidenceRotation());
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (_outliner.item == "Evidences" && _outliner.selected)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                OpenAndClose();
            }
        }

        EvidenceUpdate();
    }

    private void EvidenceUpdate()
    {
        _name.text = evidences[_evidenceIndex].evidenceName;
        _description.text = evidences[_evidenceIndex].evidenceDescription;
        _mesh.mesh = evidences[_evidenceIndex].evidenceMesh;
        _renderer.material = evidences[_evidenceIndex].evidenceMaterial;
    }

    private IEnumerator EvidenceRotation()
    {   while (true)
        {
            _rotation += 0.2f;
            _mesh.transform.localRotation = Quaternion.Euler(0, _rotation, 0);
            yield return new WaitForSeconds(0.01f);
        }
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

    public void OpenAndClose()
    {
        if (_window.activeSelf)
        {
            _window.SetActive(false);
            _gameManager.playerCanMove = true;
        }
        else
        {
            _window.SetActive(true);
            _gameManager.playerCanMove = false;
        }
    }
}
