using UnityEngine;

public class PlateformingDisable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlateformPooling plateformPooling;
    public PlateformSpawning plateformSpawning;
    [SerializeField] UIUpdate uIUpdate;
    [SerializeField] private int passedPlatformScoreAdd;
    [SerializeField] private GameObject player;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Plateform") && other.gameObject.activeInHierarchy){
            player.transform.parent = null;
            plateformPooling.ReturnToPool(other.gameObject, plateformSpawning.currentBiome);
            uIUpdate.ChangeScore(passedPlatformScoreAdd);
        }
    }
}
