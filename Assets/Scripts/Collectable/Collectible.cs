using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int randomValue;
    public Animator collectableAnimator;
    private bool disabled = false;
    DataChanger dataChanger;
    
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
                DataChanger dataChanger = collision.gameObject.GetComponent<DataChanger>();
                if (dataChanger == null)
                {
                    Debug.LogError("Ammo component not found on player.");
                    return;
                }
                dataChanger.ChangeAmmo(1);
                collectableAnimator.SetTrigger("Collect");
                if(disabled == true){
                    gameObject.SetActive(false);
                }
            }

            if(gameObject.CompareTag("Heal"))
            {
                DataChanger health = collision.gameObject.GetComponent<DataChanger>();
                if (dataChanger == null)
                {
                    Debug.LogError("Health component not found on player.");
                    return;
                }
                dataChanger.ChangeHealth(1);
                collectableAnimator.SetTrigger("Collect");
                if(disabled == true){
                    gameObject.SetActive(false);
                }
            }   
        }
    }
    public void DisableMe()
    {
        gameObject.SetActive(false);
    }


}
