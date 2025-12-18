using UnityEngine;
using UnityEngine.InputSystem;

public class OpenC : MonoBehaviour
{
    public GameObject tekst;
    public Camera arCamera;
    public GameObject slika;
    public AudioSource zvuk;
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
            if (hit.collider.CompareTag("Oprasivanje"))
            {
                ShowInfoAndPlaySound();

            }
        }
    }
    void ShowInfoAndPlaySound()
    {
        bool isActive = slika != null && slika.activeSelf;

        if (!isActive)
        {
            
            if (slika != null)
                slika.SetActive(true);

            if (tekst != null)
                tekst.SetActive(true);

           
            if (zvuk != null)
            {
                zvuk.Stop();
                zvuk.Play();
            }
        }
        else
        {
            
            if (slika != null)
                slika.SetActive(false);

            if (tekst != null)
                tekst.SetActive(false);

            
            if (zvuk != null)
                zvuk.Stop();
        }
    }

}
