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
    [SerializeField] private PlateformByBiome prefabEasyPlateform;
    [SerializeField] private PlateformByBiome prefabNormalPlateform;
    [SerializeField] private PlateformByBiome prefabHardPlateform;
    [SerializeField] private PlateformByBiome prefabExpertPlateform;



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
        
        if(prefabEasyPlateform == null || prefabEasyPlateform.plateformByBiomes.Count == 0) return;
    
        pooledEasyPlateform = new List<GameObject>();
        GameObject easyPlatform;
        int index = 0;
        
        // Parcourir les différentes couleurs de plateformes
        foreach (var plateformColor in prefabEasyPlateform.plateformByBiomes)
        {
            // Parcourir chaque préfab pour cette couleur
            foreach (var platformPrefab in plateformColor.plateformeByType)
            {
                easyPlatform = Instantiate(platformPrefab, spawningArealLocation, Quaternion.identity);
                easyPlatform.GetComponent<PlateformBehaviour>().currentIndex = index;
                easyPlatform.SetActive(false);
                easyPlatform.transform.parent = GameObject.FindGameObjectWithTag("Grid").transform;
                pooledEasyPlateform.Add(easyPlatform);
                index++;
            }
        }
    }
    void PoolingNormal()
    {
        if(prefabNormalPlateform == null || prefabNormalPlateform.plateformByBiomes.Count == 0) return;
    
        pooledNormalPlateform = new List<GameObject>();
        GameObject normalPlatform;
        int index = 0;
        
        // Parcourir les différentes couleurs de plateformes
        foreach (var plateformColor in prefabNormalPlateform.plateformByBiomes)
        {
            // Parcourir chaque préfab pour cette couleur
            foreach (var platformPrefab in plateformColor.plateformeByType)
            {
                normalPlatform = Instantiate(platformPrefab, spawningArealLocation, Quaternion.identity);
                normalPlatform.GetComponent<PlateformBehaviour>().currentIndex = index;
                normalPlatform.SetActive(false);
                normalPlatform.transform.parent = GameObject.FindGameObjectWithTag("Grid").transform;
                pooledEasyPlateform.Add(normalPlatform);
                index++;
            }
        }
    }
    void PoolingHard()
    {
        if(prefabHardPlateform == null || prefabHardPlateform.plateformByBiomes.Count == 0) return;
    
        pooledEasyPlateform = new List<GameObject>();
        GameObject hardPlatform;
        int index = 0;
        
        // Parcourir les différentes couleurs de plateformes
        foreach (var plateformColor in prefabHardPlateform.plateformByBiomes)
        {
            // Parcourir chaque préfab pour cette couleur
            foreach (var platformPrefab in plateformColor.plateformeByType)
            {
                hardPlatform = Instantiate(platformPrefab, spawningArealLocation, Quaternion.identity);
                hardPlatform.GetComponent<PlateformBehaviour>().currentIndex = index;
                hardPlatform.SetActive(false);
                hardPlatform.transform.parent = GameObject.FindGameObjectWithTag("Grid").transform;
                pooledEasyPlateform.Add(hardPlatform);
                index++;
            }
        }
    }
    void PoolingExpert()
    {
        if(prefabExpertPlateform == null || prefabExpertPlateform.plateformByBiomes.Count == 0) return;
    
        pooledEasyPlateform = new List<GameObject>();
        GameObject expertPlatform;
        int index = 0;
        
        // Parcourir les différentes couleurs de plateformes
        foreach (var plateformColor in prefabExpertPlateform.plateformByBiomes)
        {
            // Parcourir chaque préfab pour cette couleur
            foreach (var platformPrefab in plateformColor.plateformeByType)
            {
                expertPlatform = Instantiate(platformPrefab, spawningArealLocation, Quaternion.identity);
                expertPlatform.GetComponent<PlateformBehaviour>().currentIndex = index;
                expertPlatform.SetActive(false);
                expertPlatform.transform.parent = GameObject.FindGameObjectWithTag("Grid").transform;
                pooledEasyPlateform.Add(expertPlatform);
                index++;
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
