using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryScene", menuName ="Data/New Story Scene")]
[System.Serializable]
public class StoryScene : GameScene
{
    public List<Sentence> sentences;
    public Sprite background;
    public GameScene nextScene;
    public GameScene conditionScene;

    public string conditionToUnlock;

    [System.Serializable]
    public struct Sentence
    {
        public string text;
        public Speaker speaker;

        public string collectedCondition;
        public string testimony;
        public string alibi;
    }
}

public class GameScene : ScriptableObject { }
