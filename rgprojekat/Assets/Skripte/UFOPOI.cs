using UnityEngine;
using UnityEngine.SceneManagement;

public class UFOPOI : MonoBehaviour
{
    public void goToUFOScene()
    {
        SceneManager.LoadSceneAsync(5);
    }
    public void goToInfoScene()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
