using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int randomValue;
    public Animator collectableAnimator;
    public DataChanger dataChanger;
    [SerializeField] private AudioManager audioManager;
    
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    void OnEnable()
    {
        collectableAnimator = GetComponent<Animator>();
        if(gameObject.CompareTag("Heal"))
        {
            randomValue = Random.Range(0, 7);
            collectableAnimator.SetInteger("Random", randomValue);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dataChanger = collision.gameObject.GetComponent<DataChanger>();
            if(gameObject.CompareTag("Ammo"))
            {
                dataChanger.ChangeAmmo(1);
                collectableAnimator.SetTrigger("Collect");
                audioManager.PlaySfx(audioManager.itemCollected);
            }

            if(gameObject.CompareTag("Heal"))
            {
                dataChanger.ChangeHealth(1);
                collectableAnimator.SetTrigger("Collect");
                audioManager.PlaySfx(audioManager.itemCollected);
            }   
        }
    }
    public void DisableMe()
    {
        gameObject.SetActive(false);
    }


}
