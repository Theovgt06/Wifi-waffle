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
    private Evolution evolution;


    void Start()
    {
        evolution = GameObject.FindGameObjectWithTag("Managers").GetComponent<Evolution>();
        currentSpeed = evolution.currentSpeed;
        platformEffector = GetComponent<PlatformEffector2D>();
        if (platformEffector != null)
        {
            platformEffector.useOneWay = true;
        }
        if(WhatSpawn() == "Enemy")
        {
            GameObject enemies  = spawnAnchorToPool[GetRandomAnchor(spawnAnchorToPool.Count)].transform.GetChild(0).gameObject;
            enemies.transform.GetChild(GetRandomEnemies()).gameObject.SetActive(true);
        }else if(WhatSpawn() == "Collectable")
        {
            GameObject collectables = spawnAnchorToPool[GetRandomAnchor(spawnAnchorToPool.Count)].transform.GetChild(1).gameObject;
            collectables.transform.GetChild(GetRandomCollectable()).gameObject.SetActive(true);
        }else if(WhatSpawn() == "Nothing") 
        {
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
        return Random.Range(0, anchorSize); 

    }

    private int GetRandomEnemies()
    {
        return Random.Range(0,3);
    }

    private int GetRandomCollectable()
    {
        return Random.Range(0,2);
    }
    

    private string WhatSpawn()
    {
        switch (currentBiome)
        {
            case Biome.Easy:
                return EasyChances();
            case Biome.Normal:
                return NormalChances();
            case Biome.Hard:
                return HardChances();
            default:
                return "none";
        }
    }


    private string EasyChances()
    {
        return easyTable[Random.Range(0, easyTable.Count)];
    }   

    private string NormalChances()
    {
        return normalTable[Random.Range(0, normalTable.Count)];
    }

    private string HardChances()
    {
        return hardTable[Random.Range(0, hardTable.Count)];
    }





    // Weighted Lists
    private List<string> easyTable = new List<string>() 
    {
        // Easy (35% / 20% / 45%)
        "Collectable", "Collectable", "Collectable", "Collectable",
        "Collectable", "Collectable", "Collectable",

        "Enemy", "Enemy","Enemy","Enemy", 

        "Nothing", "Nothing", "Nothing", "Nothing", "Nothing",
        "Nothing", "Nothing", "Nothing", "Nothing"
    };
    private List<string> normalTable = new List<string>()
    {
        //  Normal (25% / 30% / 45%)
        "Collectable", "Collectable", "Collectable", "Collectable", "Collectable",

        "Enemy", "Enemy", "Enemy", "Enemy", "Enemy", "Enemy",

        "Nothing", "Nothing", "Nothing", "Nothing", "Nothing",
        "Nothing", "Nothing", "Nothing", "Nothing"
    };
    private List<string> hardTable = new List<string>()
    {
        // Hard (15% / 55% / 30%)
        "Collectable", "Collectable", "Collectable",

        "Enemy", "Enemy", "Enemy", "Enemy", "Enemy",
        "Enemy", "Enemy", "Enemy", "Enemy", "Enemy",
        "Enemy",

        "Nothing", "Nothing", "Nothing", "Nothing", "Nothing",
        "Nothing"
    };
}  
