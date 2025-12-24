using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    public static SfxPlayer Instance { get; private set; }

    [SerializeField] private AudioSource source;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (source == null)
            source = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        if (clip == null || source == null) return;
        source.PlayOneShot(clip);
    }
}
