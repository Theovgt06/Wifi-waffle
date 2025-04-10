using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                Destroy(gameObject);
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
                Destroy(gameObject);
            }   
        }
    }
}
