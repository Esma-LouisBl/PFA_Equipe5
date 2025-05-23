using UnityEngine;

public class InspectorController : MonoBehaviour
{
    public SpriteRenderer inspectorSprite;
    public CursorAspect cursorAspect;
    public Texture2D mainCursor, dialogueCursor;

    public StoryScene inspectorScene;

    public bool inspectorTalking = false;

    [SerializeField]
    private GameController _gameController;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private Animator _animator;

    private void Start()
    {
        cursorAspect.interactCursor = mainCursor;
    }

    private void Update()
    {
        if (cursorAspect.interactCursor == dialogueCursor && _gameManager.inspectorAble)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _gameController.currentScene = inspectorScene;
                _gameController.restart = true;
                _gameManager.inspectorAble = false;
                inspectorTalking = true;
            }
        }
    }

    public void ShowInspector()
    {
        _animator.SetTrigger("Show");
        _gameManager.inspectorAble = true;
    }

    public void HideInspector()
    {
        _animator.SetTrigger("Hide");
        inspectorTalking = false;
    }

    public void ChangeCursor()
    {
        if (cursorAspect.interactCursor == mainCursor)
        {
            cursorAspect.interactCursor = dialogueCursor;
        }

        else if (cursorAspect.interactCursor == dialogueCursor)
        {
            cursorAspect.interactCursor = mainCursor;
        }
    }

    public void CollectScene(StoryScene scene)
    {
        inspectorScene = scene;
    }
}
