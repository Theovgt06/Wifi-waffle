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
        [SerializeField] private PauseManager pauseManager;
        [SerializeField] private AudioManager audioManager;
        private Animator anim;


        void Start()
        {
            anim = GetComponent<Animator>();
            currentHealth = startingHealth;
            currentAmmo = startingAmmo; 
            uIUpdate = GameObject.FindGameObjectWithTag("Managers").GetComponent<UIUpdate>();
            if(gameObject.CompareTag("Player"))
            {
                pauseManager.isDead = false;
            }else{
                gameObject.GetComponent<Collider2D>().enabled = true;
                gameObject.GetComponent<EnemySystem>().enabled = true;
            }
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
            pauseManager.isDead = true;
            audioManager.PlaySfx(audioManager.death);
            uIUpdate.SetBestScore();
            LevelManager.Instance.GameOver();
            GameObject.Find("Player").SetActive(false);
        } 
        
        public void EnemyDying()
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<EnemySystem>().enabled = false;
        }
        
        public void EnemyDied()
        {
            gameObject.SetActive(false);
            uIUpdate.ChangeScore(killEnemyScoreAdd);
        }

    }





