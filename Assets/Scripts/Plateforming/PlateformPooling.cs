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

    [Header("Inactive List")]
    [SerializeField] private List<int> inactiveEasyIndices;
    [SerializeField] private List<int> inactiveNormalIndices;
    [SerializeField] private List<int> inactiveHardIndices;
    [SerializeField] private List<int> inactiveExpertIndices;
    
    private List<GameObject> spawnAnchorPooling;
    private List<GameObject> enemiesListPooling;
    private List<GameObject> collectableListPooling;

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
        InitializeInactiveLists();
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
                InstantiateEverything(easyPlatform);
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
                pooledNormalPlateform.Add(normalPlatform);
                index++;
            }
        }
    }
    void PoolingHard()
    {
        if(prefabHardPlateform == null || prefabHardPlateform.plateformByBiomes.Count == 0) return;
    
        pooledHardPlateform = new List<GameObject>();
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
                pooledHardPlateform.Add(hardPlatform);
                index++;
            }
        }
    }
    void PoolingExpert()
    {
        if(prefabExpertPlateform == null || prefabExpertPlateform.plateformByBiomes.Count == 0) return;
    
        pooledExpertPlateform = new List<GameObject>();
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
                pooledExpertPlateform.Add(expertPlatform);
                index++;
            }
        }
    }

    void InitializeInactiveLists()
    {
        inactiveEasyIndices = new List<int>();
        inactiveNormalIndices = new List<int>();
        inactiveHardIndices = new List<int>();
        inactiveExpertIndices = new List<int>();
        
        // Remplir avec tous les indices initiaux (toutes les plateformes sont inactives au début)
        for (int i = 0; i < pooledEasyPlateform.Count; i++)
            inactiveEasyIndices.Add(i);
        
        for (int i = 0; i < pooledNormalPlateform.Count; i++)
            inactiveNormalIndices.Add(i);
        
        for (int i = 0; i < pooledHardPlateform.Count; i++)
            inactiveHardIndices.Add(i);
        
        for (int i = 0; i < pooledExpertPlateform.Count; i++)
            inactiveExpertIndices.Add(i);
    }

    
    public GameObject GetPooledPlateformeEasy(Vector2 position)
    {
        if (inactiveEasyIndices.Count == 0)
        return null; // Pas de plateformes dispo
    
        int randomInactiveIndex = Random.Range(0, inactiveEasyIndices.Count); // Index aléatoire
        int platformIndex = inactiveEasyIndices[randomInactiveIndex]; // Récupé
        
        // Retirer cet index de la liste des inactifs
        inactiveEasyIndices.RemoveAt(randomInactiveIndex);
        // Activer la plateforme
        pooledEasyPlateform[platformIndex].transform.position = position;
        pooledEasyPlateform[platformIndex].SetActive(true);
        
        return pooledEasyPlateform[platformIndex];
    }
     public GameObject GetPooledPlateformeNormal(Vector2 position)
    {
        if (inactiveNormalIndices.Count == 0)
        return null; // Pas de plateformes dispo
    
        int randomInactiveIndex = Random.Range(0, inactiveNormalIndices.Count); // Index aléatoire
        int platformIndex = inactiveNormalIndices[randomInactiveIndex]; // Récupé
        
        // Retirer cet index de la liste des inactifs
        inactiveNormalIndices.RemoveAt(randomInactiveIndex);
        // Activer la plateforme
        pooledNormalPlateform[platformIndex].transform.position = position;
        pooledNormalPlateform[platformIndex].SetActive(true);
        
        return pooledNormalPlateform[platformIndex];
    }
     public GameObject GetPooledPlateformeHard(Vector2 position)
    {
        if (inactiveHardIndices.Count == 0)
        return null; // Pas de plateformes dispo
    
        int randomInactiveIndex = Random.Range(0, inactiveHardIndices.Count); // Index aléatoire
        int platformIndex = inactiveHardIndices[randomInactiveIndex]; // Récupé
        
        // Retirer cet index de la liste des inactifs
        inactiveHardIndices.RemoveAt(randomInactiveIndex);
        // Activer la plateforme
        pooledHardPlateform[platformIndex].transform.position = position;
        pooledHardPlateform[platformIndex].SetActive(true);
        
        return pooledHardPlateform[platformIndex];
    }

     public GameObject GetPooledPlateformeExpert(Vector2 position)
    {
        if (inactiveExpertIndices.Count == 0)
        return null; // Pas de plateformes dispo
    
        int randomInactiveIndex = Random.Range(0, inactiveExpertIndices.Count); // Index aléatoire
        int platformIndex = inactiveExpertIndices[randomInactiveIndex]; // Récupé
        
        // Retirer cet index de la liste des inactifs
        inactiveExpertIndices.RemoveAt(randomInactiveIndex);
        // Activer la plateforme
        pooledExpertPlateform[platformIndex].transform.position = position;
        pooledExpertPlateform[platformIndex].SetActive(true);
        
        return pooledExpertPlateform[platformIndex];
    }

    public void ReturnToPool(GameObject plateform, Biome currentBiome)
    {
        plateform.SetActive(false);
        int index = plateform.GetComponent<PlateformBehaviour>().currentIndex;
        
        // Ajouter l'index à la liste appropriée des indices inactifs
        switch (currentBiome)
        {
            case Biome.Easy:
                if (!inactiveEasyIndices.Contains(index))
                    inactiveEasyIndices.Add(index);
                break;
            case Biome.Normal:
                if (!inactiveNormalIndices.Contains(index))
                    inactiveNormalIndices.Add(index);
                break;
            case Biome.Hard:
                if (!inactiveHardIndices.Contains(index))
                    inactiveHardIndices.Add(index);
                break;
            case Biome.Expert:
                if (!inactiveExpertIndices.Contains(index))
                    inactiveExpertIndices.Add(index);
                break;
        }    
    }
    
    public void InstantiateEverything(GameObject platform)
    {
        spawnAnchorPooling = platform.GetComponent<PlateformBehaviour>().spawnAnchorToPool;
        enemiesListPooling = platform.GetComponent<PlateformBehaviour>().enemiesList;
        collectableListPooling = platform.GetComponent<PlateformBehaviour>().collectableList;
        foreach (GameObject spawnAnchor in spawnAnchorPooling)
        { 
            Vector2 spawnAnchorPos = spawnAnchor.transform.position;
            foreach(GameObject enemy in enemiesListPooling){
                GameObject mob = Instantiate(enemy,spawnAnchorPos, Quaternion.identity);
                mob.transform.parent = spawnAnchor.transform.GetChild(0).transform;
                mob.SetActive(false);
            }
            foreach(GameObject collectable in collectableListPooling){
                GameObject collect = Instantiate(collectable,spawnAnchorPos, Quaternion.identity);
                collect.transform.SetParent(spawnAnchor.transform.GetChild(1).transform);
                collect.SetActive(false);
            }
            platform.GetComponent<PlateformBehaviour>().spawnAnchorPooled.Add(spawnAnchor);
        }
        
    }
    
}
