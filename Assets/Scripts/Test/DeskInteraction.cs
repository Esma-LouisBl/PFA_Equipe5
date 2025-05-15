using UnityEngine;

public class DeskInteraction : MonoBehaviour
{
    [SerializeField]
    private MeshOutliner _outliner;

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

        if (_outliner.selectedDesk == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && GameManager.Instance.PlayerHasEvidence)
            {
                _currentEvidence = Instantiate(_playerHandler.CurrentEvidence, _dropPoint);
                _playerHandler.DropEvidence();
                // + Jouer un texte. A la fin de ce texte, l'objet est détruit 
                //Destroy(_currentEvidence);
            }
        }
    }
}
