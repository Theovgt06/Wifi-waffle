using UnityEngine;


public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPause;
    [SerializeField]
    private GameObject mouseIndicator;
    private void Awake()
    {
        Resume();
    }

    private void Paused()
    {
        menuPause.SetActive(true);
        mouseIndicator.SetActive(false);
        Cursor.visible = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;
        menuPause.SetActive(true);
        Debug.Log("Game Paused");

    }
    private void Resume()
    {
        menuPause.SetActive(false);
        mouseIndicator.SetActive(true);
        Cursor.visible = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        menuPause.SetActive(false);
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
