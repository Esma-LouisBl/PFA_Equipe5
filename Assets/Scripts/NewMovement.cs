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
    private float _speed;

    private void Update()
    {
        if (_outliner.selectedCabinet == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                MoveToCabinet();
            }
        }
    }

    private void MoveToCabinet()
    {
        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        while (Vector3.Distance(_playerTransform.position, _waypoint.position) > 0.01f)
        {
            _playerTransform.position = Vector3.MoveTowards(transform.position, _waypoint.position, _speed * Time.deltaTime);

            if (Vector3.Distance(_playerTransform.position, _waypoint.position) < 0.01f)
            {
                _playerTransform.position = _waypoint.position;
                //isMoving = false;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
