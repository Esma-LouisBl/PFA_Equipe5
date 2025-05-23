using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryScene", menuName = "Data/New Story Scene")]
[System.Serializable]
public class StoryScene : GameScene
{
    public List<Sentence> sentences;
    [HideInInspector]
    public Sprite background;
    public GameScene nextScene;
    public GameScene conditionScene;

    public List<string> conditionToUnlock;

    public bool callInspector;
    public bool endInspector;

    [System.Serializable]
    public struct Sentence
    {
        public string text;
        public Speaker speaker;

        public string collectedCondition;
        public TestimonyData testimony;
        public SuspectData suspect;
        public PhoneContact phoneContact;
        public PhoneContact contactToRemove;
        public EvidenceData evidence;
        public StoryScene inspectorSceneToCollect;
        public Frame photoFrame;

        public bool showSprite;
        public bool hideSprite;

        public bool destroyEvidence;

    }
}

public class GameScene : ScriptableObject { }