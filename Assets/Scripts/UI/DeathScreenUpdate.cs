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
    
    void OnAwake()
    {
        gameObject.SetActive(false);
    }
    void OnActive()
    {
        mouseIndicator.SetActive(false);
        Cursor.visible = true;
        Time.timeScale = 0f;
        currentScore.text =  uIUpdate.actualScore.ToString();
        bestScore.text = uIUpdate.bestScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
