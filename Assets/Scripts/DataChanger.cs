using UnityEngine;

public class DataChanger : MonoBehaviour
{
    [SerializeField] public int maxAmmo;
    [SerializeField] public int currentAmmo;
    [SerializeField]  private int startingAmmo;
    [SerializeField] private int maxHealth;
    [SerializeField] public int currentHealth;
    [SerializeField] private int startingHealth;
    [SerializeField] private int startingScore;
    [SerializeField] private int maxScore;
    [SerializeField] public int actualScore;
    [SerializeField] public int bestScore;
    [SerializeField] private int getCollectableScoreAdd;
    [SerializeField] private int killEnemyScoreAdd;
    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        currentAmmo = startingAmmo; 
        actualScore = startingScore;
    }

    public void ChangeAmmo(int amount)
    {
        currentAmmo += amount;
        if(amount>0)
        {
            ChangeScore(getCollectableScoreAdd);
        }
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
        if(amount>0){
            ChangeScore(getCollectableScoreAdd);
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
            anim.SetTrigger("Die");
        }
    }


    public void ChangeScore(int amount)
    {
        if(actualScore+amount >= maxScore){
            actualScore = maxScore;
            return;
        }
        actualScore+= amount;
    }

    public void PlayerDied()
    {
        LevelManager.Instance.GameOver();
        GameObject.Find("Player").SetActive(false);
        bestScore = actualScore;
    } 
    
    public void EnemyDied()
    {
        gameObject.SetActive(false);
        ChangeScore(killEnemyScoreAdd);
    }

}





