using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BottomBarController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public TextMeshProUGUI barText;
    public TextMeshProUGUI personNameText;

    private int sentenceIndex = -1;
    private StoryScene currentScene;
    private State state = State.COMPLETED;
    private Animator animator;
    private bool isHidden = false;

    [SerializeField]
    private TestimoniesController testimoniesController;
    [SerializeField]
    private ConditionsController conditionsController;

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

        spriteRenderer.sprite = currentScene.sentences[sentenceIndex].speaker.speakerSprite;

        CollectTestimonies();
        CollectConditions();
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
                if (!testimoniesController.testimoniesPeter.Contains(currentScene.sentences[sentenceIndex].testimony))  //Check if the testimony has ever been collected
                {
                    testimoniesController.testimoniesPeter.Add(currentScene.sentences[sentenceIndex].testimony);
                    testimoniesController.UploadPeter();
                }
            }

            if (currentScene.sentences[sentenceIndex].speaker.speakerName == "Holly")   //Same for Holly
            {
                if (!testimoniesController.testimoniesHolly.Contains(currentScene.sentences[sentenceIndex].testimony))
                {
                    testimoniesController.testimoniesHolly.Add(currentScene.sentences[sentenceIndex].testimony);
                    testimoniesController.UploadHolly();
                }
            }

            if (currentScene.sentences[sentenceIndex].speaker.speakerName == "Oliver")  //Same for Oliver
            {
                if (!testimoniesController.testimoniesOliver.Contains(currentScene.sentences[sentenceIndex].testimony))
                {
                    testimoniesController.testimoniesOliver.Add(currentScene.sentences[sentenceIndex].testimony);
                    testimoniesController.UploadOliver();
                }
            }
        }
    }

    public void CollectConditions()
    {
        if (currentScene.sentences[sentenceIndex].collectedCondition != "")
        {
            if (!conditionsController.collectedConditions.Contains(currentScene.sentences[sentenceIndex].collectedCondition))
            {
                conditionsController.collectedConditions.Add(currentScene.sentences[sentenceIndex].collectedCondition);
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
            barText.text += text[wordIndex];
            yield return new WaitForSeconds(0.05f);
            if(++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }
}
