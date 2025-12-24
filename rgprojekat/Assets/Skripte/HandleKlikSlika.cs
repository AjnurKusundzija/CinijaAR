using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class HandleKlikSlika : MonoBehaviour
{
    [Header("Slika (Plane)")]
    public GameObject plane;

    [Header("Video screen (Velika kocka)")]
    public GameObject videoScreen;

    [Header("AR / Main Camera")]
    public Camera arCamera;

    private VideoPlayer videoPlayer;
    private InputAction tapAction;

    [Header("SFX")]
    [SerializeField] private AudioClip clickClip;          // kratki klik (one-shot)
    [SerializeField] private AudioSource drugiZvukSource;  // duži zvuk (toggle Play/Stop)

    // klju?na varijabla
    private bool isShown = false;

    void Awake()
    {
        tapAction = new InputAction(
            type: InputActionType.Button,
            binding: "<Pointer>/press"
        );

        if (videoScreen != null)
            videoPlayer = videoScreen.GetComponent<VideoPlayer>();
    }

    void OnEnable()
    {
        tapAction.Enable();
        tapAction.performed += OnTap;
    }

    void OnDisable()
    {
        tapAction.performed -= OnTap;
        tapAction.Disable();
    }

    void OnTap(InputAction.CallbackContext ctx)
    {
        Vector2 pos = Pointer.current.position.ReadValue();
        Ray ray = arCamera.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // RADI i ako je tag na parentu, a collider na childu
            Transform t = hit.collider.transform;
            while (t != null && !t.CompareTag("Slika"))
                t = t.parent;

            if (t != null)
            {
                // 1) kratki klik (uvijek)
                if (SfxPlayer.Instance != null && clickClip != null)
                    SfxPlayer.Instance.Play(clickClip);

                // 2) toggle "drugog" (dužeg) zvuka
                if (drugiZvukSource != null)
                {
                    if (drugiZvukSource.isPlaying) drugiZvukSource.Stop();
                    else drugiZvukSource.Play();
                }

                // 3) tvoja postoje?a logika
                ToggleImageAndVideo();
            }
        }
    }

    void ToggleImageAndVideo()
    {
        isShown = !isShown;

        // Slika
        if (plane != null)
            plane.SetActive(isShown);

        // Video screen
        if (videoScreen != null)
            videoScreen.SetActive(isShown);

        // Video
        if (videoPlayer != null)
        {
            if (isShown)
            {
                videoPlayer.Stop();
                videoPlayer.time = 0;
                videoPlayer.Play();
            }
            else
            {
                videoPlayer.Stop();
            }
        }
    }
}
