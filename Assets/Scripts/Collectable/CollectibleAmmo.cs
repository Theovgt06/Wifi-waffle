using UnityEngine;

public class CollectibleAmmo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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

    }
}
