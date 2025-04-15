using UnityEngine;

public class DataChanger : MonoBehaviour
{
    [SerializeField] public int maxAmmo;
    [SerializeField] public int currentAmmo;
    [SerializeField]  private int startingAmmo;
    [SerializeField] private int maxHealth;
    [SerializeField] public int currentHealth;
    [SerializeField] private int startingHealth;
    [SerializeField] private int getCollectableScoreAdd;
    [SerializeField] private int killEnemyScoreAdd;
    [SerializeField] private UIUpdate uIUpdate;
    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        currentAmmo = startingAmmo; 
        uIUpdate = GameObject.FindGameObjectWithTag("Managers").GetComponent<UIUpdate>();
    }

    public void ChangeAmmo(int amount)
    {
        currentAmmo += amount;
        if(amount>0)
        {
            uIUpdate.ChangeScore(getCollectableScoreAdd);
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
            uIUpdate.ChangeScore(getCollectableScoreAdd);
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
        {
            anim.SetTrigger("Die");
        }
    }


    

    public void PlayerDied()
    {
        LevelManager.Instance.GameOver();
        GameObject.Find("Player").SetActive(false);
        uIUpdate.SetBestScore();
    } 
    
    public void EnemyDied()
    {
        gameObject.SetActive(false);
        uIUpdate.ChangeScore(killEnemyScoreAdd);
    }

}





