using UnityEngine;
using UnityEngine.SceneManagement;


public class KolibriPOI : MonoBehaviour
{
    public void LoadKolibriScene() { 
    
        SceneManager.LoadSceneAsync(6);
    }
    public void Info()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
