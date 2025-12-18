using UnityEngine;

public class ChangeVolume : MonoBehaviour
{
    public static ChangeVolume Instance;

    private AudioSource audioSource;

    private const string VolumeKey = "MusicVolume";
    private const string MusicStateKey = "MusicPlaying";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();

            // U?itaj glasno?u (default 50%)
            float volume = PlayerPrefs.GetFloat(VolumeKey, 0.5f);
            audioSource.volume = volume;

            audioSource.loop = true;
            audioSource.playOnAwake = false;

            // U?itaj stanje muzike (svira / ne svira)
            bool wasPlaying = PlayerPrefs.GetInt(MusicStateKey, 0) == 1;
            if (wasPlaying)
                audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ?? Klik na Music dugme (Main screen)
    public void PlayMusicAndGoToSettings()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            PlayerPrefs.SetInt(MusicStateKey, 1);
        }
    }

    // ??? Slider (Music scene)
    public void SetVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }

    // ?? Back dugme (Music scene)
    public void BackToMain()
    {
        // ništa ne gasimo – muzika ostaje
        PlayerPrefs.SetInt(MusicStateKey, audioSource.isPlaying ? 1 : 0);
    }

    public bool IsMusicPlaying()
    {
        return audioSource.isPlaying;
    }

    public float GetVolume()
    {
        return audioSource.volume;
    }
}
