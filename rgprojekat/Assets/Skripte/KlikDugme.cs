using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class KlikDugme : MonoBehaviour
{
    [SerializeField] private AudioClip clickClip;

    private Button btn;

    void Awake()
    {
        btn = GetComponent<Button>();
    }

    void OnEnable()
    {
        // bitno: kad se UI gasi/pali ili scene reload, da ne dodaje više puta isti listener
        btn.onClick.RemoveListener(PlayClick);
        btn.onClick.AddListener(PlayClick);
    }

    void OnDisable()
    {
        btn.onClick.RemoveListener(PlayClick);
    }

    private void PlayClick()
    {
        if (SfxPlayer.Instance == null) return;
        SfxPlayer.Instance.Play(clickClip);
    }
}
