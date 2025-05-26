using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NewMovement : MonoBehaviour
{
    [SerializeField]
    private Animator _animation;

    [SerializeField]
    private Transform _playerTransform;
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
    private GameObject _fromTableButton, _fromBoardButton;

    [SerializeField]
    private float _speed = 20f;
    [SerializeField]
    private float _rotationSpeed = 350f;
    //private Quaternion _targetRotation;

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
        //StartCoroutine(Moving(_waypointCabinet, true));

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
        //StartCoroutine(Moving(_waypointTable, false));

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

        //StartCoroutine(Moving(_waypointDesk, false));

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

        //StartCoroutine(Moving(_waypointDesk, true));

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
        _fromTableButton.SetActive(false);
    }
    public void ShowCanvasTable()
    {
        if (canShowCanvas)
        {
            _fromTableButton.SetActive(true);
            canShowCanvas = false;
        }
    }


    public void WantToMove(string triggerName)
    {
        EnableCursor(false);
        _animation.SetTrigger("" + triggerName);
    }

    public void MovePlayerToCabinet()
    {
        _playerTransform.position = _waypointCabinet.position;
        _playerTransform.rotation = _waypointCabinet.rotation;
        EnableCursor(true);
    }

    public void MovePlayerToTable()
    {
        _playerTransform.position = _waypointTable.position;
        _playerTransform.rotation = _waypointTable.rotation;
        EnableCursor(true);
    }

    public void MovePlayerToDesk()
    {
        _playerTransform.position = _waypointDesk.position;
        _playerTransform.rotation = _waypointDesk.rotation;
        EnableCursor(true);
    }

    public void EnableCursor(bool condition)
    {
        _cursorController.EnableCursor(condition);
    }

    //private IEnumerator Moving(Transform waypoint, bool goRight)
    //{
    //    _cursorController.EnableCursor(false);

    //    while (Vector3.Distance(_playerTransform.position, waypoint.position) > 0.01f)
    //    {
    //        _playerTransform.position = Vector3.MoveTowards(_playerTransform.position, waypoint.position, _speed * Time.deltaTime);

    //        if (Vector3.Distance(_playerTransform.position, waypoint.position) < 0.01f)
    //        {
    //            _playerTransform.position = waypoint.position;
    //        }
    //        yield return new WaitForSeconds(0.01f);
    //    }

    //    if (goRight)
    //    {
    //        _targetRotation = Quaternion.Euler(0f, _playerTransform.eulerAngles.y + 90f, 0f);
    //    }
    //    else
    //    {
    //        _targetRotation = Quaternion.Euler(0f, _playerTransform.eulerAngles.y - 90f, 0f);
    //    }

    //    while (Quaternion.Angle(_playerTransform.rotation, _targetRotation) > 1f)
    //    {
    //        _playerTransform.rotation = Quaternion.RotateTowards(_playerTransform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);

    //        if (Quaternion.Angle(_playerTransform.rotation, _targetRotation) < 1f)
    //        {
    //            _playerTransform.rotation = _targetRotation;

    //            _cursorController.EnableCursor(true);
    //        }
    //        yield return new WaitForSeconds(0.005f);
    //    }

    //}
}
