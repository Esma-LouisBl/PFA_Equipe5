using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NewMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private Transform _waypoint;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private MeshOutliner _outliner;
    [SerializeField]
    private CursorController _cursorController;

    [SerializeField]
    private float _speed = 20f;
    [SerializeField]
    private float _rotationSpeed = 350f;
    private Quaternion _targetRotation;

    [SerializeField]
    private GameObject _realCabinet, _falseCabinet, _realEvidenceBook, _falseEvidencebook, _realTestimonyBook, _falseTestimonyBook, _realSuspectBook, _falseSuspectBook, _realBoard, _falseBoard;

    private void Update()
    {
        if (_outliner.selectedCabinet == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _gameManager.playerCanMove)
            {
                MoveToCabinet();
            }
        }
    }

    private void MoveToCabinet()
    {
        StartCoroutine(Moving());

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

        _outliner.selectedCabinet = false;
    }

    private IEnumerator Moving()
    {
        _cursorController.EnableCursor(false);
        while (Vector3.Distance(_playerTransform.position, _waypoint.position) > 0.01f)
        {
            _playerTransform.position = Vector3.MoveTowards(_playerTransform.position, _waypoint.position, _speed * Time.deltaTime);

            if (Vector3.Distance(_playerTransform.position, _waypoint.position) < 0.01f)
            {
                _playerTransform.position = _waypoint.position;
                //isMoving = false;
            }
            yield return new WaitForSeconds(0.01f);
        }

        _targetRotation = Quaternion.Euler(0f, _playerTransform.eulerAngles.y + 90f, 0f);

        while (Quaternion.Angle(_playerTransform.rotation, _targetRotation) > 1f)
        {
            _playerTransform.rotation = Quaternion.RotateTowards(_playerTransform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
            
            if (Quaternion.Angle(_playerTransform.rotation, _targetRotation) < 1f)
            {
                _playerTransform.rotation = _targetRotation;

                _cursorController.EnableCursor(true);
            }
            yield return new WaitForSeconds(0.005f);
        }
    }
}
