using UnityEngine;
using System.Collections.Generic;
public class PlateformPooling : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public static PlateformPooling SharedInstance;
    private GameObject spawningArea;
    private Vector2 spawningArealLocation;

    [Header("Pooled List")] 
    [SerializeField]private List<GameObject> pooledEasyPlateform;
    [SerializeField]private List<GameObject> pooledNormalPlateform;
    [SerializeField]private List<GameObject> pooledHardPlateform;
    [SerializeField]private List<GameObject> pooledExpertPlateform;


    [Header("Prefab List")]
    [SerializeField] private List<List<GameObject>> prefabEasyPlateform;
    [SerializeField] private List<List<GameObject>> prefabNormalPlateform;
    [SerializeField] private List<List<GameObject>> prefabHardPlateform;
    [SerializeField] private List<List<GameObject>> prefabExpertPlateform;


    void Awake()
    {
        SharedInstance = this;
        spawningArea = GameObject.FindGameObjectWithTag("Spawning Area");
        spawningArealLocation = spawningArea.transform.position;
    }


    void Start()
    {     
        PoolingEasy();
        PoolingNormal();
        PoolingHard();
        PoolingExpert();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PoolingEasy()
    {
        if((prefabEasyPlateform.Count == 0) && (prefabEasyPlateform[0].Count == 0)) return;
        pooledEasyPlateform = new List<GameObject>();
        GameObject easyPlatform;
        foreach (List<GameObject> i in prefabEasyPlateform) //the line the error is pointing to
        {
            foreach (GameObject j in i) //the line the error is pointing to
            {
                easyPlatform = Instantiate(j,spawningArealLocation, Quaternion.identity);
                easyPlatform.SetActive(false);
                pooledEasyPlateform.Add(easyPlatform);
            }
        }
    }
    void PoolingNormal()
    {
        if((prefabNormalPlateform.Count == 0) && (prefabNormalPlateform[0].Count == 0)) return;
        pooledNormalPlateform = new List<GameObject>();
        GameObject normalPlatform;
        foreach (List<GameObject> i in prefabNormalPlateform) //the line the error is pointing to
        {
            foreach (GameObject j in i) //the line the error is pointing to
            {
                normalPlatform = Instantiate(j,spawningArealLocation, Quaternion.identity);
                normalPlatform.SetActive(false);
                pooledNormalPlateform.Add(normalPlatform);
            }
        } 
    }
    void PoolingHard()
    {
        if((prefabHardPlateform.Count == 0) && (prefabHardPlateform[0].Count == 0)) return;
        pooledHardPlateform = new List<GameObject>();
        GameObject hardPlatform;
        foreach (List<GameObject> i in prefabHardPlateform) //the line the error is pointing to
        {
            foreach (GameObject j in i) //the line the error is pointing to
            {
                hardPlatform = Instantiate(j,spawningArealLocation, Quaternion.identity);
                hardPlatform.SetActive(false);
                pooledHardPlateform.Add(hardPlatform);
            }
        }
    }
    void PoolingExpert()
    {
        if((prefabExpertPlateform.Count == 0) && (prefabExpertPlateform[0].Count == 0)) return;
         pooledExpertPlateform = new List<GameObject>();
        GameObject expertPlatform;
        foreach (List<GameObject> i in prefabExpertPlateform) //the line the error is pointing to
        {
            foreach (GameObject j in i) //the line the error is pointing to
            {
                expertPlatform = Instantiate(j,spawningArealLocation, Quaternion.identity);
                expertPlatform.SetActive(false);
                pooledExpertPlateform.Add(expertPlatform);
            }
        } 
    }



    public GameObject GetPooledPlateformeEasy(Vector2 position)
    {
        int j = Random.Range(0, pooledEasyPlateform.Count-1);
        if(!pooledEasyPlateform[j].activeInHierarchy)
        {
            pooledEasyPlateform[j].transform.position = position;
            pooledEasyPlateform[j].SetActive(true);
            return pooledEasyPlateform[j];
        }
        return null;
    }

   
    
}
