using UnityEngine;
using UnityEngine.SceneManagement;

public class RenderInfo : MonoBehaviour
{
    public void GoToRenderInfo()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void GoBackToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}




