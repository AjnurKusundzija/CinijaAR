using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class HandleKlikSunce : MonoBehaviour
{
    public GameObject tekst;
    public Camera arCamera;
    // ? Vuforia / AR kamera
    [SerializeField] private AudioClip clickClip;
    private InputAction tapAction;

    void Awake()
    {
        tapAction = new InputAction(
            type: InputActionType.Button,
            binding: "<Pointer>/press"
        );
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
            // 1) Na?i root koji nosi tag Sun (radi i kad collider nije na tagovanom objektu)
            Transform root = hit.collider.transform;
            while (root != null && !root.CompareTag("Sun"))
                root = root.parent;

            if (root != null) // zna?i pogodio si nešto unutar Sunca
            {
                if (SfxPlayer.Instance != null && clickClip != null)
                    SfxPlayer.Instance.Play(clickClip);

                if (tekst != null)
                    tekst.SetActive(!tekst.activeSelf);
            }
        }
    }

}
