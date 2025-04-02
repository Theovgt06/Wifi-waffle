using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPooling : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    public static BulletPooling SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public GameObject player;
    public int maxAmountToPool;


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

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < maxAmountToPool; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
}
}
