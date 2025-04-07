using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPooling : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private GameObject pooledBulletList;
    private List<GameObject> pooledObjects;
    [SerializeField] 
    private GameObject objectToPool;


    public int maxAmountToPool; // Futur réference à Ammo.


    void Awake()
    {
    }



    void Start()
    {
        pooledBulletList = GameObject.FindGameObjectWithTag("BulletList");
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0 ; i<maxAmountToPool; i ++){
            tmp = Instantiate(objectToPool, transform.position, Quaternion.identity);
            tmp.SetActive(false);
            tmp.transform.SetParent(pooledBulletList.transform);
            pooledObjects.Add(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledObject(Vector2 position)
    {
        int inactiveCount = 0;
        
        for(int i = 0; i < maxAmountToPool; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                inactiveCount++;
                pooledObjects[i].transform.position = position;
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }   
        return null;
}

}
