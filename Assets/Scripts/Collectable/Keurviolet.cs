using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public int playerHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                playerHealth += 1;
            }

        }
        
    }
}
