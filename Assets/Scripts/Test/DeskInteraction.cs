using UnityEngine;

public class DeskInteraction : MonoBehaviour
{
    [SerializeField]
    private Handler _playerHandler;
    [SerializeField]
    private GameObject _currentEvidence;

    [SerializeField]
    private Transform _dropPoint;

    public void Update()
    {
        if (GameManager.Instance.PlayerHasEvidence)
        {
            gameObject.tag = "Selectable";
        }
        else
        {
            gameObject.tag = "Untagged";
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && GameManager.Instance.PlayerHasEvidence)
        {
            _currentEvidence = Instantiate(_playerHandler.CurrentEvidence, _dropPoint);
            _playerHandler.DropEvidence();
        }
    }
}
