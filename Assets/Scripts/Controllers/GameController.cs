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

    private enum State
    {
        IDLE, ANIMATE, CHOOSE
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
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (_state == State.IDLE && bottomBar.IsCompleted())
            {
                if (bottomBar.IsLastSentence())
                {
                    if ((currentScene as StoryScene).conditionToUnlock == "")   //if there is no condition for the next Scene
                    {
                        PlayScene((currentScene as StoryScene).nextScene);  //play the Scene "nextScene"
                    }
                    else
                    {
                        if (conditionsController.collectedConditions.Contains((currentScene as StoryScene).conditionToUnlock))  //if there is a condition for the next Scene and the player completed it
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
                    bottomBar.PlayNextSentence();
                }
            }

            else if (_state == State.IDLE && !bottomBar.IsCompleted())  //click but sentence isn't complete yet
            {
                bottomBar.Interrupt();
            }
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
