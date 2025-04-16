using UnityEngine;

public class SpawningPlatformDespawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject player;
    [SerializeField] private float delayBeforeDespawn;
    private float actualTime;
    void Start()
    {
        actualTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        actualTime += Time.deltaTime;
        if(actualTime >= delayBeforeDespawn)
        {
            player.transform.parent = null;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D() 
    {
        player.transform.parent = null;
        gameObject.SetActive(false);
    }
}
