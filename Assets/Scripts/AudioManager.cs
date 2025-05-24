using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    [SerializeField]
    private AudioSource _musicSource, _sfxSource;

    [Header("Sounds")]
    public AudioClip _pageTurned;
    public AudioClip _hangUp;
    public AudioClip _pickUp;
    public AudioClip _Phone01;
    public AudioClip _Phone02;
    public AudioClip _Phone03;

    [Header("Themes")]
    public AudioClip _mainMenuTheme;
    public AudioClip _investigationTheme;
    public AudioClip _endingTheme;
    public AudioClip _inspectorTheme;
    public AudioClip _warnerTheme;
    public AudioClip _hollyTheme;
    public AudioClip _scootTheme;
    public AudioClip _witnessTheme;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        PlayMusic(_mainMenuTheme);
    }

    public void PlayMusic(AudioClip music)
    {
        _musicSource.Stop();
        _musicSource.clip = music;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip sfx)
    {
        _sfxSource.Stop();
        _sfxSource.clip = sfx;
        _sfxSource.Play();
    }


}
