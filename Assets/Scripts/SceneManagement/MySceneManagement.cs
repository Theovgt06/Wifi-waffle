using UnityEngine;

public class MySceneManagement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioManager audioManager;
    
    void Start()
    {
        audioManager = transform.GetChild(0).transform.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        // Load the game scene
        audioManager.PlaySfx(audioManager.buttons);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        
    }

     public void QuitGame()
    {
        audioManager.PlaySfx(audioManager.buttons);
        Application.Quit();

        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
