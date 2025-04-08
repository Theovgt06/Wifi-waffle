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


    void Start()
    {
        platformEffector = GetComponent<PlatformEffector2D>();
        if (platformEffector != null)
        {
            platformEffector.useOneWay = true;
        }
        // spawnAnchorToPool[GetRandomAnchor(spawnAnchorPooled.Count)].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
    }


    void OnEnable()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
        switch(currentBiome)
        {
            case Biome.Easy:
                EasyBehavior();
                break;
            case Biome.Normal:
                NormalBehavior();
                break;
            case Biome.Hard:
                HardBehavior();
                break;
            case Biome.Expert:
                ExpertBehavior();
                break;
        } 
    }
    private void EasyBehavior()
    {
        transform.position -= Vector3.up * currentSpeed * Time.deltaTime;
    }

    private void NormalBehavior()
    {

    }


    private void HardBehavior()
    {

    }


    private void ExpertBehavior()
    {

    }

    private int GetRandomAnchor(int anchorSize)
    {
        return Random.Range(0, anchorSize); 

    }

    private int GetRandomEnemies(int maxIndexEnemies,  int spawnChances)
    {
        return 0;
    }

    private int GetRandomCollectable(int spawnChances)
    {
        return 0;
    }
    
}
