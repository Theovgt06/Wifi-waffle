    using UnityEngine;


public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPause;
    [SerializeField]
    private GameObject mouseIndicator;

    [SerializeField] AudioManager audioManager;
    public bool isDead;
    private void Awake()
    {
        Resume();
    }

    private void Paused()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0f;
        audioManager.LowPassMusic(true);

    }
    private void Resume()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1f;
        audioManager.LowPassMusic(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isDead)
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
