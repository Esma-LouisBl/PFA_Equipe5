using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BottomBarController : MonoBehaviour
{
    [SerializeField]
    private float _textSpeed = 0.05f;
    public SpriteRenderer spriteRenderer;

    public TextMeshProUGUI barText;
    public TextMeshProUGUI personNameText;

    private int sentenceIndex = -1;
    private StoryScene currentScene;
    private State state = State.COMPLETED;
    private Animator animator;
    private bool isHidden = false;

    private bool _interrupted = false;

    [SerializeField]
    private TestimoniesController _testimoniesController;
    [SerializeField]
    private SuspectsController _suspectsController;
    [SerializeField]
    private ConditionsController _conditionsController;
    [SerializeField]
    private PhoneController _phoneController;
    [SerializeField]
    private GameManager _gameManager;

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
        ClearText();
        animator.SetTrigger("Show");
        isHidden = false;
    }

    public void ClearText()
    {
        barText.text = "";
    }

    public void PlayScene(StoryScene scene)
    {
        currentScene = scene;
        sentenceIndex = -1;
        PlayNextSentence();
    }

    public void PlayNextSentence()
    {
        StartCoroutine(TypeText(currentScene.sentences[++sentenceIndex].text));
        personNameText.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
        personNameText.color = currentScene.sentences[sentenceIndex].speaker.textColor;

        if (currentScene.sentences[sentenceIndex].speaker.name != "Player")    //if the sentence is prononced by the player, do not change the sprite
        {
            spriteRenderer.sprite = currentScene.sentences[sentenceIndex].speaker.speakerSprite;
        }

        if (currentScene.name == "Abandon")
        {
            _gameManager.GameOver();
        }

        CollectTestimonies();
        CollectAlibis();
        CollectConditions();
        CollectPhoneContacts();
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

    public void CollectTestimonies()
    {
        if (currentScene.sentences[sentenceIndex].testimony != "")  //Check if there's a testimony in the sentence
        {
            if (currentScene.sentences[sentenceIndex].speaker.speakerName == "Peter")   //Check from who is the testimony
            {
                if (!_testimoniesController.testimoniesPeter.Contains(currentScene.sentences[sentenceIndex].testimony))  //Check if the testimony has ever been collected
                {
                    _testimoniesController.testimoniesPeter.Add(currentScene.sentences[sentenceIndex].testimony);
                    _testimoniesController.UploadPeter();
                }
            }

            if (currentScene.sentences[sentenceIndex].speaker.speakerName == "Holly")   //Same for Holly
            {
                if (!_testimoniesController.testimoniesHolly.Contains(currentScene.sentences[sentenceIndex].testimony))
                {
                    _testimoniesController.testimoniesHolly.Add(currentScene.sentences[sentenceIndex].testimony);
                    _testimoniesController.UploadHolly();
                }
            }

            if (currentScene.sentences[sentenceIndex].speaker.speakerName == "Oliver")  //Same for Oliver
            {
                if (!_testimoniesController.testimoniesOliver.Contains(currentScene.sentences[sentenceIndex].testimony))
                {
                    _testimoniesController.testimoniesOliver.Add(currentScene.sentences[sentenceIndex].testimony);
                    _testimoniesController.UploadOliver();
                }
            }
        }
    }

    public void CollectAlibis()
    {
        if (currentScene.sentences[sentenceIndex].alibi != "")  //déclenche l'apparition de l'alibi du perso correspondant sur la fiche des suspects
        {
            _suspectsController.TurnOnAlibi(currentScene.sentences[sentenceIndex].alibi);
        }
    }

    public void CollectConditions()
    {
        if (currentScene.sentences[sentenceIndex].collectedCondition != "")
        {
            if (!_conditionsController.collectedConditions.Contains(currentScene.sentences[sentenceIndex].collectedCondition))
            {
                _conditionsController.collectedConditions.Add(currentScene.sentences[sentenceIndex].collectedCondition);
            }
        }
    }

    public void CollectPhoneContacts()
    {
        if (currentScene.sentences[sentenceIndex].phoneContact != null)
        {
            if (!_phoneController.contactList.Contains(currentScene.sentences[sentenceIndex].phoneContact))
            {
                _phoneController.contactList.Add(currentScene.sentences[sentenceIndex].phoneContact);
            }
        }
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
                yield return new WaitForSeconds(_textSpeed);
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
