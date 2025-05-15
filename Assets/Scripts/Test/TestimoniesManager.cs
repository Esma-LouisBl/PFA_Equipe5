using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestimoniesManager : MonoBehaviour
{
    private List<TestimonyData> unlockedTestimonies = new List<TestimonyData>();
    private int currentIndex;

    [SerializeField]
    private Image _leftArrow;
    [SerializeField]
    private Image _rightArrow;
    [SerializeField]
    private Image _testimonyLeft;
    [SerializeField]
    private Image _testimonyRight;

    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private TextMeshProUGUI _description;

    public void UnlockedTestimony(TestimonyData newTestimony)
    {
        if (!unlockedTestimonies.Contains(newTestimony))    //check if the testimony has already been collected
        {
            unlockedTestimonies.Add(newTestimony);
            UIUpdate();
        }
    }

    private void UIUpdate()
    {
        _name.text = unlockedTestimonies[currentIndex].Name;
        _description.text = unlockedTestimonies[currentIndex].Description;

        bool canUseLeft = currentIndex > 0;
        bool canUseRight = currentIndex < unlockedTestimonies.Count - 1;

        _leftArrow.gameObject.SetActive(canUseLeft);
        _testimonyLeft.gameObject.SetActive(canUseLeft);

        _rightArrow.gameObject.SetActive(canUseRight);
        _testimonyRight.gameObject.SetActive(canUseRight);
    }

    public void NextTestimony()
    {
        if (currentIndex < unlockedTestimonies.Count - 1)
        {
            currentIndex++;
        }
        else
        {
            currentIndex = 0;
        }
        UIUpdate();
    }
    
    public void PreviousTestimony()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }
        else
        {
            currentIndex = unlockedTestimonies.Count - 1;
        }
        UIUpdate();
    }
}
