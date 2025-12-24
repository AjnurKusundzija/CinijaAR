using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kviz : MonoBehaviour
{
    
    public void LoadKviz()
    {
        SceneManager.LoadSceneAsync(7);
    }
    public void LoadMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
