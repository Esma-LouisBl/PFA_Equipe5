using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TestimoniesController : MonoBehaviour
{
    public List<string> testimoniesPeter;
    public List<string> testimoniesHolly;
    public List<string> testimoniesOliver;

    [SerializeField]
    private TextMeshProUGUI _textPeter, _textHolly, _textOliver;
    [SerializeField]
    private GameObject _window, _peter, _holly, _oliver;

    private int _witnessIndex;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_window.activeSelf)
            {
                _window.SetActive(false);
            }
            else
            {
                _window.SetActive(true);
            }
        }

        if (_witnessIndex == 0)
        {
            _peter.SetActive(true);
            _oliver.SetActive(false);
            _holly.SetActive(false);
        }

        if (_witnessIndex == 1)
        {
            _peter.SetActive(false);
            _oliver.SetActive(true);
            _holly.SetActive(false);
        }

        if (_witnessIndex == 2)
        {
            _peter.SetActive(false);
            _oliver.SetActive(false);
            _holly.SetActive(true);
        }
    }
    public void UploadPeter()
    {
        _textPeter.text = "";
        foreach (string testimony in testimoniesPeter)
        {
            _textPeter.text += "- ";
            _textPeter.text += testimony;
            _textPeter.text += "\n";
        }
    }

    public void UploadHolly()
    {
        _textHolly.text = "";
        foreach (string testimony in testimoniesHolly)
        {
            _textHolly.text += testimony;
            _textHolly.text += "\n";
        }
    }
    public void UploadOliver()
    {
        _textOliver.text = "";
        foreach (string testimony in testimoniesOliver)
        {
            _textOliver.text += testimony;
            _textOliver.text += "\n";
        }
    }

    public void ChangeTestimonyUp()
    {
        if (_witnessIndex < 2)
        {
            _witnessIndex++;
        }
        else
        {
            _witnessIndex =0;
        }
    }

    public void ChangeTestimonyDown()
    {
        if (_witnessIndex > 0)
        {
            _witnessIndex--;
        }
        else
        {
            _witnessIndex = 2;
        }
    }
}
