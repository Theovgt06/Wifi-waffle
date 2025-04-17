using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;


    [SerializeField] private UiManager uIManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void GameOver()
    {
        UiManager _ui = GetComponent<UiManager>();
        if (_ui != null)
        if (uIManager != null)
        {
            _ui.ToggleDeathScreen();
            uIManager.ToggleDeathScreen();
        }
        else
        {
            Debug.LogError("UiManager component not found on LevelManager.");
        }
    }
}
