using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int randomValue;
    public Animator collectableAnimator;
    private bool disabled = false;

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
                Ammo ammo = collision.gameObject.GetComponent<Ammo>();
                if (ammo == null)
                {
                    Debug.LogError("Ammo component not found on player.");
                    return;
                }
                ammo.ChangeAmmo(1);
                collectableAnimator.SetTrigger("Collect");
                if(disabled == true){
                    gameObject.SetActive(false);
                }
            }

            if(gameObject.CompareTag("Heal"))
            {
                Health health = collision.gameObject.GetComponent<Health>();
                if (health == null)
                {
                    Debug.LogError("Health component not found on player.");
                    return;
                }
                health.ChangeHealth(1);
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
