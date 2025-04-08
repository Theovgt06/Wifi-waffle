using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateformBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int currentIndex;
    public float currentSpeed;
    private PlatformEffector2D platformEffector;
    public Biome currentBiome;
    [SerializeField] private  float enemySpawnChances;
    [SerializeField] private  float collectableSpawnChances;

    
    [Header("Spawn Anchors")]
    [SerializeField]
    public List<GameObject> spawnAnchorToPool = new List<GameObject>();

    [Header("Enemies List")]
    [SerializeField]
    public List<GameObject> enemiesList = new List<GameObject>();
    [Header("Collectable List")]
    [SerializeField]
    public List<GameObject> collectableList = new List<GameObject>();

    public List<GameObject> spawnAnchorPooled = new List<GameObject>();

    private int maxLevel;
    private int numberOfPoolInstantiated;


    void Start()
    {
        platformEffector = GetComponent<PlatformEffector2D>();
        if (platformEffector != null)
        {
            platformEffector.useOneWay = true;
        }
        numberOfPoolInstantiated = GameObject.FindGameObjectWithTag("Managers").GetComponent<Evolution>().spawnedPlatform;
        maxLevel = GameObject.FindGameObjectWithTag("Managers").GetComponent<Evolution>().maxLevel;
        if(WhatSpawn(maxLevel,numberOfPoolInstantiated) == "enemy" && numberOfPoolInstantiated%2==0)
        {
            GameObject enemies  = spawnAnchorToPool[GetRandomAnchor(spawnAnchorToPool.Count)].transform.GetChild(0).gameObject;
            enemies.transform.GetChild(GetRandomEnemies()).gameObject.SetActive(true);
        }else if(WhatSpawn(maxLevel,numberOfPoolInstantiated) == "collectable" &&  numberOfPoolInstantiated%2==0)
        {
            GameObject collectables = spawnAnchorToPool[GetRandomAnchor(spawnAnchorToPool.Count)].transform.GetChild(1).gameObject;
            collectables.transform.GetChild(GetRandomCollectable()).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.up * currentSpeed * Time.deltaTime;
       
    }

    void OnEnable()
        {
        GameObject.FindGameObjectWithTag("Managers").GetComponent<Evolution>().spawnedPlatform++;
    }
    private int GetRandomAnchor(int anchorSize)
    {
        return Random.Range(0, anchorSize-1); 

    }

    private int GetRandomEnemies()
    {
        return Random.Range(0,2);
    }

    private int GetRandomCollectable()
    {
        return Random.Range(0,1);
    }
    

    private string WhatSpawn(int highestLevel, int currentPlatformCount)
    {
        
        // Valeurs initiales (difficultyIndex = 0)
        float baseEnemyProb = 20f;
        float baseCollectableProb = 20f;
        
        // Valeurs maximales (difficultyIndex = 800)
        float maxEnemyProb = 95f;
        float minCollectableProb = 5f;
        
        // Calcul du facteur de progression (entre 0 et 1)
        float progressFactor = Mathf.Min(currentPlatformCount / highestLevel, 1f);
        
        // Calcul des probabilités actuelles
        float enemyProbability = baseEnemyProb + (maxEnemyProb - baseEnemyProb) * progressFactor;
        float collectableProbability = baseCollectableProb - (baseCollectableProb - minCollectableProb) * progressFactor;
        float nothingProbability = 100f - enemyProbability - collectableProbability;
        
        // Génération aléatoire
        float randomChance = Random.Range(0, 100);
        
        if (randomChance < enemyProbability)
        {
            return "enemy";
        }
        else if (randomChance < enemyProbability + collectableProbability)
        {
            return "collectable";
        }
        else
        {
            return "none";
        }
    } 
}  
