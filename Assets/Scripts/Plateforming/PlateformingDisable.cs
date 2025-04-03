using UnityEngine;

public class PlateformingDisable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("caa");
        if(other.gameObject.CompareTag("Plateform")){
            other.gameObject.SetActive(false);
        }
    }
}
