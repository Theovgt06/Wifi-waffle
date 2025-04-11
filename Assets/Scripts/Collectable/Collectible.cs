using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int randomValue;
    public Animator collectableAnimator;
    public DataChanger dataChanger;
    
    void OnEnable()
    {
        collectableAnimator = GetComponent<Animator>();
        if(gameObject.CompareTag("Heal"))
        {
            randomValue = Random.Range(0, 7);
            collectableAnimator.SetInteger("Random", randomValue);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(gameObject.CompareTag("Ammo"))
            {
                dataChanger.ChangeAmmo(1);
                collectableAnimator.SetTrigger("Collect");

            }

            if(gameObject.CompareTag("Heal"))
            {
<<<<<<< Updated upstream
                DataChanger health = collision.gameObject.GetComponent<DataChanger>();
                if (dataChanger == null)
                {
                    Debug.LogError("Health component not found on player.");
                    return;
                }
=======
>>>>>>> Stashed changes
                dataChanger.ChangeHealth(1);
                collectableAnimator.SetTrigger("Collect");
            }   
        }
    }
    public void DisableMe()
    {
        gameObject.SetActive(false);
    }


}
