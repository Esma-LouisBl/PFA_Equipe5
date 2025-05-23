using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsText : MonoBehaviour
{
    [SerializeField] 
    private List<OptionsDialogue> _allOptions;

    [SerializeField]
    private TextMeshProUGUI _text;

    private int index;

    void Start()
    {
        index = 1;
        _text.text = _allOptions[index].Text;
        SettingsManager.Instance.TextSpeed = _allOptions[index].ValueSpeed;
    }

    public void NextOption()
    {
        {
            if (index < _allOptions.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }

            _text.text = _allOptions[index].Text;
            SettingsManager.Instance.TextSpeed = _allOptions[index].ValueSpeed;
        }
    }

    public void PreviousOption()
    {
        if (index > 0)
        {
            index--;
        }
        else
        {
            index = _allOptions.Count - 1;
        }

        _text.text = _allOptions[index].Text;
        SettingsManager.Instance.TextSpeed = _allOptions[index].ValueSpeed;
    }
}
