using System.Collections;
using TMPro;
using UnityEngine;

public class EvidencesController : MonoBehaviour
{

    [SerializeField]
    private GameObject _window;
    [SerializeField]
    private TextMeshProUGUI _name, _description;
    [SerializeField]
    private MeshFilter _mesh;
    [SerializeField]
    private MeshRenderer _renderer;

    private float _rotation;


    [SerializeField]
    private MeshOutliner _outliner;

    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private EvidencesSystem _evidencesSystem;
    [SerializeField]
    private GameObject _returnButton;

    private void Start()
    {
        StartCoroutine(EvidenceRotation());
    }

    void Update()
    {
        if (_outliner.selectedEvidences == true)
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
        _name.text = _evidencesSystem.currentEvidence.Name;
        _description.text = _evidencesSystem.currentEvidence.Informations;
        _mesh.mesh = _evidencesSystem.currentEvidence.EvidenceGO.GetComponent<MeshFilter>().sharedMesh;
        _renderer.material = _evidencesSystem.currentEvidence.EvidenceGO.GetComponent<MeshRenderer>().sharedMaterial;
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
        _evidencesSystem.ChangeEvidenceUp();
        PlayASound();
    }

    public void ChangeEvidenceDown()
    {
        _evidencesSystem.ChangeEvidenceDown();
        PlayASound();
    }

    public void OpenAndClose()
    {
        if (_window.activeSelf)
        {
            _window.SetActive(false);
            _gameManager.readingNote = false;
            _returnButton.SetActive(true);
        }
        else
        {
            if (!_gameManager.readingNote)
            {
                _window.SetActive(true);
                _gameManager.readingNote = true;
                _returnButton.SetActive(false);
            }
        }
    }

    public void PlayASound()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance._pageTurned);
    }
}
