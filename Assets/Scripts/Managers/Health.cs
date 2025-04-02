using JetBrains.Annotations;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private const int MAX_HEALTH = 7;
    [SerializeField] private int currentHealth;
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > MAX_HEALTH)
        {
            currentHealth = MAX_HEALTH;
        }
        else if (currentHealth <= 0)
        {
            //a voir quoi faire UnityEvent
        }
    }

}
