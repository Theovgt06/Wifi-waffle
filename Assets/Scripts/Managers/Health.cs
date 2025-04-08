using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 7;
    [SerializeField] private int currentHealth;
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            //a voir quoi faire un Eventzrrzrzr
        }
    }
   
}
