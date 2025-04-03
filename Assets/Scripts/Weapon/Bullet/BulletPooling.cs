using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPooling : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    public static BulletPooling SharedInstance;
    private List<GameObject> pooledObjects;
    [SerializeField] 
    private GameObject objectToPool;
    private GameObject player;

    public int maxAmountToPool; // Futur réference à Ammo.


    void Awake()
    {
        SharedInstance = this;
        player = GameObject.Find("Player");
    }



    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0 ; i<maxAmountToPool; i ++){
            tmp = Instantiate(objectToPool, player.transform.position, player.transform.rotation);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledObject(Vector2 position)
    {
        for(int i = 0; i < maxAmountToPool; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].transform.position = position;
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }
        return null;
    }
}
