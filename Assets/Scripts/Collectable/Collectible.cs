using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int randomValue;
    public Animator collectableAnimator;

    void Start()
    {
        collectableAnimator = GetComponent<Animator>();

        // Random seulement au début pour définir une animation
        randomValue = Random.Range(0, 7);
        collectableAnimator.SetInteger("Random", randomValue);
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
                Debug.Log("CACA");
                collectableAnimator.SetTrigger("Collect");
            }   
        }
    }
    void Disable()
    {
        gameObject.SetActive(false);
    }

}
