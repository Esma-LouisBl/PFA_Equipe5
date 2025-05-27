using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator.SetTrigger("Credits");
        StartCoroutine(QuitCredits());
    }

    private IEnumerator QuitCredits()
    {
        yield return new WaitForSeconds(16f);
        SceneManager.LoadSceneAsync(0);
    }
}
