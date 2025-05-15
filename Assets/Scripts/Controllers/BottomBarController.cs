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
    private TestimoniesManager _testimoniesManager;
    [SerializeField]
    private SuspectsManager _suspectsManager;

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
        CollectSuspects();
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
        if (currentScene.sentences[sentenceIndex].testimony != null)  //Check if there's a testimony in the sentence
        {
            _testimoniesManager.UnlockedTestimony(currentScene.sentences[sentenceIndex].testimony);
        }
    }

    public void CollectSuspects()
    {
        if (currentScene.sentences[sentenceIndex].suspect != null)
        {
            _suspectsManager.UnlockedEvidence(currentScene.sentences[sentenceIndex].suspect);
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
