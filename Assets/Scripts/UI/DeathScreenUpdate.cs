using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenUpdate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI bestScore;
    [SerializeField] private UIUpdate uIUpdate;
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Evolution evolution;
    
    void Awake()
    {
        Cursor.visible = false;
        gameObject.SetActive(false);

    }
    void OnEnable()
    {
        Time.timeScale = 0f;
        evolution.spawnedPlatform = 0;
        mouseIndicator.SetActive(false);
        Cursor.visible = true;
        audioManager.PlayMusic("MainScene", audioManager.backgroundnoise);
        currentScore.text =  uIUpdate.actualScore.ToString();
        bestScore.text = uIUpdate.bestScore.ToString();

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        audioManager.PlaySfx(audioManager.buttons);
        // audioManager.PlayMusic("MainScene", audioManager.gamemusic);
        Time.timeScale = 1f;
        Destroy(evolution.gameObject);
        SceneManager.LoadScene("MainScene");
    }

    public void ReturnToMenu()
    {
        audioManager.PlaySfx(audioManager.buttons);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
