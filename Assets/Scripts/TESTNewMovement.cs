using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TESTNewMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;

    [SerializeField]
    private Animator _animation;
    
    [SerializeField]
    private Transform _waypointCabinet, _waypointTable, _waypointDesk;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private InspectorController _inspectorController;
    [SerializeField]
    private MeshOutliner _outliner;
    [SerializeField]
    private CursorController _cursorController;
    [SerializeField]
    private GameObject _fromtTableButton, _fromBoardButton;

    private Quaternion _targetRotation;

    public bool canShowCanvas;

    [SerializeField]
    private GameObject _realCabinet, _falseCabinet, _realEvidenceBook, _falseEvidencebook, _realTestimonyBook, _falseTestimonyBook, _realSuspectBook, _falseSuspectBook, _realBoard, _falseBoard, _realTable, _falseTable;

    private void Update()
    {
        if (_outliner.selectedCabinet == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _gameManager.playerCanMove)
            {
                MoveToCabinet();
                WantToMove("DeskToCabinet");
            }
        }

        if (_outliner.selectedTable == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _gameManager.playerCanMove)
            {
                MoveToTable();
                WantToMove("DeskToTable");
            }
        }
    }

    private void MoveToCabinet()
    {
        _realCabinet.SetActive(false);
        _falseCabinet.SetActive(true);

        _realEvidenceBook.SetActive(true);
        _falseEvidencebook.SetActive(false);

        _realSuspectBook.SetActive(true);
        _falseSuspectBook.SetActive(false);

        _realTestimonyBook.SetActive(true);
        _falseTestimonyBook.SetActive(false);

        _realBoard.SetActive(true);
        _falseBoard.SetActive(false);

        _fromBoardButton.SetActive(true);

        _outliner.selectedCabinet = false;
    }
    private void MoveToTable()
    {

        _realTable.SetActive(false);
        _falseTable.SetActive(true);

        canShowCanvas = true;
        ShowCanvasTable();

        _inspectorController.ChangeCursor();

        _outliner.selectedTable = false;
    }

    public void ReturnDesk()
    {
        _fromBoardButton.SetActive(false);

        _realCabinet.SetActive(true);
        _falseCabinet.SetActive(false);

        _realEvidenceBook.SetActive(false);
        _falseEvidencebook.SetActive(true);

        _realSuspectBook.SetActive(false);
        _falseSuspectBook.SetActive(true);

        _realTestimonyBook.SetActive(false);
        _falseTestimonyBook.SetActive(true);

        _realBoard.SetActive(false);
        _falseBoard.SetActive(true);

        _realTable.SetActive(true);
        _falseTable.SetActive(false);
    }

    public void ReturnDeskFromTable()
    {
        HideCanvasTable();

        _inspectorController.ChangeCursor();

        _realCabinet.SetActive(true);
        _falseCabinet.SetActive(false);

        _realEvidenceBook.SetActive(false);
        _falseEvidencebook.SetActive(true);

        _realSuspectBook.SetActive(false);
        _falseSuspectBook.SetActive(true);

        _realTestimonyBook.SetActive(false);
        _falseTestimonyBook.SetActive(true);

        _realBoard.SetActive(false);
        _falseBoard.SetActive(true);

        _realTable.SetActive(true);
        _falseTable.SetActive(false);
    }

    public void HideCanvasTable()
    {
        _fromtTableButton.SetActive(false);
    }
    public void ShowCanvasTable()
    {
        if (canShowCanvas)
        {
            _fromtTableButton.SetActive(true);
            canShowCanvas = false;
        }
    }
   

    public void WantToMove(string triggerName)
    {
        _cursorController.EnableCursor(false);
        _animation.SetTrigger("" + triggerName);
    }

    public void MovingToTheRight () //For Animations
    {
        _targetRotation = Quaternion.Euler(0f, _playerTransform.eulerAngles.y + 90f, 0f);
        _playerTransform.rotation = _targetRotation;
        _playerTransform.position = _waypointCabinet.position;
        _cursorController.EnableCursor(true);
    }

    public void MovingToTheLeft(GameObject waypoint)
    {
        _targetRotation = Quaternion.Euler(0f, _playerTransform.eulerAngles.y - 90f, 0f);
        _playerTransform.rotation = _targetRotation;
        _playerTransform.position = waypoint.transform.position;
        _cursorController.EnableCursor(true);
    }

}
