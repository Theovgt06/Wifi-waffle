using UnityEngine;

public class DataChanger : MonoBehaviour
{
    [SerializeField] public int maxAmmo;
    [SerializeField] public int currentAmmo;
<<<<<<< Updated upstream
    [SerializeField] public  int startingAmmo = 5;
=======
>>>>>>> Stashed changes
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
        else if (currentAmmo <= 0)
        {
            currentAmmo = 0;
        }
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            anim.SetTrigger("Die");
        }
    }
    private void PlayerDied()
    {
        // Appeler la méthode de gestion de la mort ici
        LevelManager.Instance.GameOver();
        GameObject.Find("Player").SetActive(false);
    }


}





