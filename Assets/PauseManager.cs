using UnityEngine;
using UnityEngine.WSA;
using static UnityEngine.Rendering.DebugUI;


public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pause;
    private void Start()
    {
        Resume();
    }

    private void Paused()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        pause.SetActive(true);
        Debug.Log("Game Paused");

    }
    private void Resume()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        pause.SetActive(false);
        Debug.Log("Game Resumed");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
            {
                Paused();
            }
            else if (Time.timeScale == 0f)
            {
                Resume();
            }
        }
       
        
    }
}
