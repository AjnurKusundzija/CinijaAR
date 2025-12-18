using UnityEngine;
using UnityEngine.SceneManagement;

public class SuncePOI : MonoBehaviour
{
    public void OnSunClick()
    {
        SceneManager.LoadSceneAsync(4);
    }
    public void BackInfo()
        {
        SceneManager.LoadSceneAsync(2);
    }
}
