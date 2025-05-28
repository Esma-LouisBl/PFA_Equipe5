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

    [SerializeField]
    private BottomBarController _bottomBarController;
    [SerializeField]
    private GameController _gameController;
    [SerializeField]
    private EvidencesSystem _evidencesSystem;

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

                if (_bottomBarController.currentScene.somebodyAtDesk)

                {
                    if (_bottomBarController.GetCurrentSpeaker() == "Peter Warner")
                    {
                        LoadScene(_evidencesSystem.currentEvidence.ReactionPeter);
                    }
                    if (_bottomBarController.GetCurrentSpeaker() == "Holly Marie")
                    {
                        LoadScene(_evidencesSystem.currentEvidence.ReactionHolly);
                    }
                    if (_bottomBarController.GetCurrentSpeaker() == "Oliver Scott")
                    {
                        LoadScene(_evidencesSystem.currentEvidence.ReactionOliver);
                    }
                    if (_bottomBarController.GetCurrentSpeaker() == "Inspecteur Gavin")
                    {
                        LoadScene(_evidencesSystem.currentEvidence.ReactionInspector);
                    }
                }
                else if (_bottomBarController.currentScene.somebodyAtDesk == false)
                {
                    DestroyEvidence();
                }
            }
        }
    }

    private void LoadScene(StoryScene scene)
    {
        _gameController.currentScene = scene;
        _gameController.restart = true;
    }

    public void DestroyEvidence()
    {
        Destroy(_currentEvidence);
    }
}
