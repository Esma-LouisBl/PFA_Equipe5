using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameScene currentScene;
    public BottomBarController bottomBar;
    public BackgroundController backgroundController;
    public ChooseController chooseController;

    public ConditionsController conditionsController;

    private State _state = State.IDLE;
    public bool isActive, restart;

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
                                Debug.Log("i = " + i);
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
        }
        else
        {
            isActive = true;
        }

        if (restart == true)
        {
            _state = State.RESTART;
            restart = false;
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
}
