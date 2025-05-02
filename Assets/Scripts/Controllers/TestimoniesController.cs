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
    private GameObject _window;

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
}
