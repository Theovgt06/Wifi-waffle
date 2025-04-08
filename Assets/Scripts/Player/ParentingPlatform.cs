using UnityEngine;

public class ParentingPlatform : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("touch");
        if (collision.gameObject.CompareTag("Plateform"))
        {
            transform.parent.transform.parent = collision.transform;
            Debug.Log("touch");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("touchplu");
        if (collision.gameObject.CompareTag("Plateform"))
            {
                transform.parent.transform.parent = null;
                Debug.Log("touchplu");
            }
    }
}
