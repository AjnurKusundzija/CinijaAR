using UnityEngine;

public class Suncez : MonoBehaviour
{
    [SerializeField] private AudioClip clickClip;

    public void PlayClick()
    {
        if (SfxPlayer.Instance != null && clickClip != null)
            SfxPlayer.Instance.Play(clickClip);
    }
}
