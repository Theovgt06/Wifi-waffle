using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject DeathScreen;

    public void ToggleDeathScreen()
    {
        DeathScreen.SetActive(true);
    }

   
}
