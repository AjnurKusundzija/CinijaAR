using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoScena : MonoBehaviour
{
   public void GoBackToMainMenu()
   {
       SceneManager.LoadSceneAsync(0);

    }
    public void GotoInfoPage()
    {
        SceneManager.LoadSceneAsync(2);
    }

}
