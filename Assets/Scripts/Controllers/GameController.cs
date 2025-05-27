using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameScene currentScene;
    public BottomBarController bottomBar;
    public BackgroundController backgroundController;
    public ChooseController chooseController;

    public ConditionsController conditionsController;
    public InspectorController inspectorController;
    public NewMovement newMovement;

    public CursorController cursorController;

    [SerializeField]
    private Animator _blackScreenAnimator;

    [SerializeField]
    private GameObject _falseTable, _realTable, _falsePhone, _realPhone, _falseCabinet, _realCabinet;

    private State _state = State.IDLE;
    public bool isActive, restart;

    [Header("Act 4 Dusk")]
    [SerializeField]
    private MeshRenderer _window;
    [SerializeField]
    private Material _windowMaterialDusk;
    [SerializeField]
    private GameObject _baseLights, _duskLights;


    private enum State
    {
        IDLE, ANIMATE, CHOOSE, STOP, RESTART
    }

    void Start()
    {
        if (currentScene is StoryScene)
        {
            StoryScene storyScene = currentScene as StoryScene;
            bottomBar.PlayScene(storyScene);
            backgroundController.SetImage(storyScene.background);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (_state == State.IDLE && bottomBar.IsCompleted())
            {
                if (bottomBar.IsLastSentence())
                {
                    if ((currentScene as StoryScene).nextScene != null) //check if there is a scene after this one
                    {
                        if ((currentScene as StoryScene).conditionToUnlock.Count == 0)   //if there is no condition for the next Scene
                        {
                            PlayScene((currentScene as StoryScene).nextScene);  //play the Scene "nextScene"
                        }
                        else
                        {
                            bool allConditions = false;
                            int numberConditions = 0;

                            for (int i = 0; i < (currentScene as StoryScene).conditionToUnlock.Count; i++)
                            {
                                if (conditionsController.collectedConditions.Contains((currentScene as StoryScene).conditionToUnlock[i]))
                                {
                                        numberConditions++;
                                }
                            }

                            if (numberConditions == (currentScene as StoryScene).conditionToUnlock.Count)
                            {
                                allConditions = true;
                            }

                            if (allConditions == true)  //if there is a condition for the next Scene and the player completed it
                            {
                                PlayScene((currentScene as StoryScene).conditionScene); //play the Scene "conditionScene"
                            }
                            else    //if the player doesn't complete the condition
                            {
                                PlayScene((currentScene as StoryScene).nextScene);  //play the Scene "nextScene"
                            }
                        }
                    }
                    else
                    {
                        _state = State.STOP;    //DOESN'T WORK IF IT ENDS WITH A CHOOSE SCENE
                        bottomBar.Hide();

                        if ((currentScene as StoryScene).callInspector)     //check if Inspector is called
                        {
                            inspectorController.ShowInspector();
                        }
                        if ((currentScene as StoryScene).endInspector)      //check if Inspector has to go
                        {
                            inspectorController.HideInspector();
                            newMovement.canShowCanvas = true;
                            newMovement.ShowCanvasTable();
                        }

                        if ((currentScene as StoryScene).blackScreen)       //check if BlackScreen must play
                        {
                            StartCoroutine(FadeIn());
                        }

                    }
                }
                else
                {
                    bottomBar.PlayNextSentence();
                }
            }

            else if (_state == State.IDLE && !bottomBar.IsCompleted())  //click but sentence isn't complete yet
            {
                bottomBar.Interrupt();
            }
        }

        if (_state == State.STOP)
        {
            isActive = false;
            //_realTable.SetActive(true);
            //_falseTable.SetActive(false);
            _realPhone.SetActive(true);
            _falsePhone.SetActive(false);
            //_realCabinet.SetActive(true);
            //_falseCabinet.SetActive(false);
            newMovement.canShowCanvas = true;
        }
        else
        {
            isActive = true;
        }

        if (restart == true)
        {
            _state = State.RESTART;
            //_realTable.SetActive(false);
            //_falseTable.SetActive(true);
            _realPhone.SetActive(false);
            _falsePhone.SetActive(true);
            //_realCabinet.SetActive(false);
            //_falseCabinet.SetActive(true);
            restart = false;
            newMovement.HideCanvasTable();
            newMovement.canShowCanvas = false;
        }

        if (_state == State.RESTART)
        {
            _state = State.IDLE;
            StoryScene storyScene = currentScene as StoryScene;
            bottomBar.PlayScene(storyScene);
            backgroundController.SetImage(storyScene.background);

            bottomBar.Show();
        }
    }

    public void PlayScene(GameScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

    private IEnumerator SwitchScene(GameScene scene)
    {
        _state = State.ANIMATE;
        currentScene = scene;
        bottomBar.Hide();
        yield return new WaitForSeconds(1f);
        if (scene is StoryScene)
        {
            StoryScene storyScene = scene as StoryScene;
            backgroundController.SwitchImage(storyScene.background);
            yield return new WaitForSeconds(1f);
            bottomBar.ClearText();
            bottomBar.Show();
            yield return new WaitForSeconds(1f);
            bottomBar.PlayScene(storyScene);
            _state = State.IDLE;
        }
        else if (scene is ChooseScene)
        {
            _state = State.CHOOSE;
            chooseController.SetupChoose(scene as ChooseScene);
        }
    }

    private IEnumerator FadeIn()
    {
        _blackScreenAnimator.SetTrigger("Fade");
        cursorController.EnableCursor(false);
        yield return new WaitForSeconds(3.5f);
        _window.material = _windowMaterialDusk;
        _baseLights.SetActive(false);
        _duskLights.SetActive(true);
        yield return new WaitForSeconds(3f);
        cursorController.EnableCursor(true);
    }
}
