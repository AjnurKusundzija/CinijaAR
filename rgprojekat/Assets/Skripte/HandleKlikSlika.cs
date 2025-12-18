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

    // ?? klju?na varijabla
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
            if (hit.collider.CompareTag("Slika"))
            {
                ToggleImageAndVideo();
            }
        }
    }

    void ToggleImageAndVideo()
    {
        isShown = !isShown;

        // SLika
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
