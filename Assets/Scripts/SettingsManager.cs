using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public float TextSpeed;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
