using UnityEngine;

public class PlateformingDisable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlateformPooling plateformPooling;
    public PlateformSpawning plateformSpawning;
   
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Plateform")){
            plateformPooling.ReturnToPool(other.gameObject, plateformSpawning.currentBiome);
        }
    }
}
