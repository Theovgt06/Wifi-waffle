using UnityEngine;

public class DataChanger : MonoBehaviour
{
    [SerializeField] public int maxAmmo;
    [SerializeField] public int currentAmmo;
    [SerializeField] private int startingAmmo;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private int startingHealth;

    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        currentAmmo = startingAmmo; 
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

    public void PlayerDied()
    {
        LevelManager.Instance.GameOver();
        GameObject.Find("Player").SetActive(false);
    } 
    
    public void EnemyDied()
    {
        
    }

}





