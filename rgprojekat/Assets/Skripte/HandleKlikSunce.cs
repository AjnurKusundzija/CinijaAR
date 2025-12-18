using UnityEngine;
using UnityEngine.InputSystem;

public class HandleKlikSunce : MonoBehaviour
{
    public GameObject tekst;
    public Camera arCamera;   // ? Vuforia / AR kamera

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
            if (hit.collider.CompareTag("Sun"))
            {
                tekst.SetActive(!tekst.activeSelf);
            }
        }
    }
}
