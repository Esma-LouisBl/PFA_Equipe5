using System.Collections;
using UnityEngine;
using TMPro;

public class BottomBarController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public TextMeshProUGUI barText;
    public TextMeshProUGUI personNameText;

    private int _sentenceIndex = -1;
    private StoryScene currentScene;
    private State state = State.COMPLETED;
    private Animator animator;
    [SerializeField]
    private Animator _spriteAnimator;
    private bool isHidden = false;

    private bool _interrupted = false;

    [SerializeField]
    private TestimoniesManager _testimoniesManager;
    [SerializeField]
    private SuspectsManager _suspectsManager;

    [SerializeField]
    private ConditionsController _conditionsController;
    [SerializeField]
    private PhoneController _phoneController;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private EvidencesSystem _evidencesSystem;
    [SerializeField]
    private FrameController _PhotoFrame;
    [SerializeField]
    private InspectorController _inspectorController;
    [SerializeField]
    private DeskInteraction _deskInteraction;
    private enum State
    {
        PLAYING, COMPLETED
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Hide()
    {
        if (!isHidden)
        {
            animator.SetTrigger("Hide");
            isHidden = true;
        }
    }

    public void Show()
    {
        //ClearText();
        animator.SetTrigger("Show");
        isHidden = false;
    }

    public void HideSpeaker()
    {
        _spriteAnimator.SetTrigger("HideSpeaker");
    }

    public void ShowSpeaker()
    {
        _spriteAnimator.SetTrigger("ShowSpeaker");
    }

    public void ClearText()
    {
        barText.text = "";
    }

    public void PlayScene(StoryScene scene)
    {
        currentScene = scene;
        _sentenceIndex = -1;
        PlayNextSentence();
    }

    public void PlayNextSentence()
    {
        StartCoroutine(TypeText(currentScene.sentences[++_sentenceIndex].text));
        personNameText.text = currentScene.sentences[_sentenceIndex].speaker.speakerName;
        personNameText.color = currentScene.sentences[_sentenceIndex].speaker.textColor;

        if (currentScene.sentences[_sentenceIndex].speaker.name != "Player")    //if the sentence is prononced by the player, do not change the sprite
        {
            if (_inspectorController.inspectorTalking)
            {
                _inspectorController.inspectorSprite.sprite = currentScene.sentences[_sentenceIndex].speaker.speakerSprite;
            }
            else
            {
                spriteRenderer.sprite = currentScene.sentences[_sentenceIndex].speaker.speakerSprite;
            }
        }

        if (currentScene.sentences[_sentenceIndex].showSprite)
        {
            ShowSpeaker();
        }
        if (currentScene.sentences[_sentenceIndex].hideSprite)
        {
            HideSpeaker();
        }

        CollectTestimonies();
        CollectSuspects();
        CollectConditions();
        CollectPhoneContacts();
        CollectEvidences();
        CollectFrame();
        CollectInspectorScene();

        DestroyEvidence();
        
        RemoveContact();
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public bool IsLastSentence()
    {
        return _sentenceIndex + 1 == currentScene.sentences.Count;
    }

    public void CollectTestimonies()
    {
        if (currentScene.sentences[_sentenceIndex].testimony != null)  //Check if there's a testimony in the sentence
        {
            _testimoniesManager.UnlockedTestimony(currentScene.sentences[_sentenceIndex].testimony);
        }
    }

    public void CollectSuspects()
    {
        if (currentScene.sentences[_sentenceIndex].suspect != null)
        {
            _suspectsManager.UnlockedEvidence(currentScene.sentences[_sentenceIndex].suspect);
        }
    }

    public void CollectConditions()
    {
        if (currentScene.sentences[_sentenceIndex].collectedCondition != "")
        {
            if (!_conditionsController.collectedConditions.Contains(currentScene.sentences[_sentenceIndex].collectedCondition))
            {
                _conditionsController.collectedConditions.Add(currentScene.sentences[_sentenceIndex].collectedCondition);
            }
        }
    }

    public void CollectPhoneContacts()
    {
        if (currentScene.sentences[_sentenceIndex].phoneContact != null)
        {
            if (!_phoneController.contactList.Contains(currentScene.sentences[_sentenceIndex].phoneContact))
            {
                _phoneController.contactList.Add(currentScene.sentences[_sentenceIndex].phoneContact);
            }
        }
    }

    public void RemoveContact()
    {
        if (currentScene.sentences[_sentenceIndex].contactToRemove != null)
        {
            if (_phoneController.contactList.Contains(currentScene.sentences[_sentenceIndex].contactToRemove))
            {
                _phoneController.contactList.Remove(currentScene.sentences[_sentenceIndex].contactToRemove);
            }
        }
    }

    public void CollectEvidences()
    {
        if (currentScene.sentences[_sentenceIndex].evidence != null)
        {
            _evidencesSystem.AddEvidence(currentScene.sentences[_sentenceIndex].evidence);
        }
    }

    public void DestroyEvidence()
    {
        if (currentScene.sentences[_sentenceIndex].destroyEvidence)
        {
            _deskInteraction.DestroyEvidence();
        }
    }

    public void CollectFrame()
    {
        var frameData = currentScene.sentences[_sentenceIndex].photoFrame;
        if (frameData != null)
        {
            
            _PhotoFrame.ShowFrame(frameData);
            
        }
    }

    public void CollectInspectorScene()
    {
        if (currentScene.sentences[_sentenceIndex].inspectorSceneToCollect != null)
        {
            _inspectorController.CollectScene(currentScene.sentences[_sentenceIndex].inspectorSceneToCollect);
        }
    }


    public string GetCurrentSpeaker()
    {
        string currentSpeakerName;
        currentSpeakerName = (currentScene.sentences[_sentenceIndex].speaker.speakerName);
        return currentSpeakerName;
    }

    private IEnumerator TypeText(string text)
    {
        barText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;

        while (state != State.COMPLETED)
        {
            if (!_interrupted)
            {
                barText.text += text[wordIndex];
                yield return new WaitForSeconds(SettingsManager.Instance.TextSpeed);
                if (++wordIndex == text.Length)
                {
                    state = State.COMPLETED;
                    break;
                }
            }

            else
            {
                barText.text = text;
                state = State.COMPLETED;
                _interrupted = false;
                break;
            }
        }
    }

    public void Interrupt()
    {
        _interrupted = true;
    }
}
