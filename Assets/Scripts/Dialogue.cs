using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int _index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[_index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[_index];
            }
        }
    }

    public void StartDialogue()
    {
        _index = 0;
        StartCoroutine(TypeLine());
    }

    public IEnumerator TypeLine()
    {
        foreach (char c in lines[_index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (_index < lines.Length - 1)
        {
            _index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
