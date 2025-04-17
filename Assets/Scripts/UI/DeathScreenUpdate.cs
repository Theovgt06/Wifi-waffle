using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        gameObject.SetActive(false);

    }
    void OnEnable()
    {
        Time.timeScale = 0f;
        evolution.spawnedPlatform = 0;
        audioManager.musicSource.clip = audioManager.backgroundnoise;
        currentScore.text =  uIUpdate.actualScore.ToString();
        bestScore.text = uIUpdate.bestScore.ToString();

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        audioManager.musicSource.clip = audioManager.gamemusic;
        Time.timeScale = 1f;
        Destroy(evolution.gameObject);
        SceneManager.LoadScene("MainScene");
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
