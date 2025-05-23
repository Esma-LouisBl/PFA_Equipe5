using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    [SerializeField]
    private AudioSource _musicSource, _sfxSource;

    [Header("Sounds")]
    [SerializeField]
    private AudioClip _hangUp;
    [SerializeField]
    private AudioClip _pickUp;
    [SerializeField]
    private AudioClip _Phone01;
    [SerializeField]
    private AudioClip _Phone02;
    [SerializeField]
    private AudioClip _Phone03;

    [Header("Themes")]
    [SerializeField]
    private AudioClip _mainMenuTheme;
    [SerializeField]
    private AudioClip _investigationTheme;
    [SerializeField]
    private AudioClip _endingTheme;
    [SerializeField]
    private AudioClip _inspectorTheme;
    [SerializeField]
    private AudioClip _warnerTheme;
    [SerializeField]
    private AudioClip _hollyTheme;
    [SerializeField]
    private AudioClip _scootTheme;
    [SerializeField]
    private AudioClip _witnessTheme;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic(_mainMenuTheme);
    }

    void Update()
    {
        
    }

    public void PlayMusic(AudioClip music)
    {
        _musicSource.Stop();
        _musicSource.clip = music;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip sfx)
    {
        _musicSource.Stop();
        _musicSource.clip = sfx;
        _musicSource.Play();
    }


}
