using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
  public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ChangeSceneByName(string sceneName)
    {
       if (name!= null) { 
        SceneManager.LoadScene(sceneName);
        }
    }
}
