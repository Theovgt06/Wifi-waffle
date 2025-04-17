using UnityEngine;

public class StartButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
