using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPooling : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    private List<GameObject> pooledObjects;
    [SerializeField] 
    private GameObject objectToPool;


    public int maxAmountToPool; // Futur réference à Ammo.


    void Awake()
    {
    }



    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0 ; i<maxAmountToPool; i ++){
            tmp = Instantiate(objectToPool, transform.position, Quaternion.identity);
            tmp.SetActive(false);
            tmp.transform.SetParent(gameObject.transform);
            pooledObjects.Add(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledObject(Vector2 position)
    {
        Debug.Log($"Trying to get pooled object at position {position}");
        int inactiveCount = 0;
        
        for(int i = 0; i < maxAmountToPool; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                inactiveCount++;
                Debug.Log($"Found inactive bullet at index {i}");
                pooledObjects[i].transform.position = position;
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }
        
        Debug.LogWarning($"No inactive bullets found in pool. Total inactive: {inactiveCount}/{maxAmountToPool}");
        return null;
}

}
