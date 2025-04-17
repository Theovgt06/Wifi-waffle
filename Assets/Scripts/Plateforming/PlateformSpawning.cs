using System;
using UnityEngine;


public enum Biome {Stop, Easy, Normal, Hard};

public class PlateformSpawning : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created    
    private GameObject spawningArea;
    public Biome currentBiome;
    public Evolution evolutionScript;
    public float spawnRate; 
    private float previousSpawnRate;
    void Start()
    {
        spawningArea = GameObject.FindGameObjectWithTag("Spawning Area");
        evolutionScript = GameObject.FindGameObjectWithTag("Managers").GetComponent<Evolution>();
        previousSpawnRate = spawnRate;
    }

    void OnEnable()
    {
        currentBiome = evolutionScript.currentBiome;
        spawnRate = evolutionScript.spawnRate;
        previousSpawnRate = spawnRate;
        UpdateSpawnRate();
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
        previousSpawnRate = spawnRate;
    }
    void Update()
    {
        UpdateSpawnRate();
    }
    void UpdateSpawnRate()
    {
        currentBiome = evolutionScript.currentBiome;
        spawnRate = evolutionScript.spawnRate;
        // Si le spawnRate a changé, on relance le InvokeRepeating
        if (spawnRate > 0 && Mathf.Abs(spawnRate - previousSpawnRate) >= 0.01f)
        {
            CancelInvoke(nameof(Spawn));
            InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
            previousSpawnRate = spawnRate;
        }
    }

    // Update is called once per frame
    void Spawn()
    {
        switch(currentBiome)
        {
            case Biome.Stop:
                StopSpawning();
                break;
            case Biome.Easy:
                EasySpawning();
                break;
            case Biome.Normal:
                NormalSpawning();
                break;
            case Biome.Hard:
                HardSpawning();
                break;
        }
    }


    void StopSpawning()
    {
        CancelInvoke(nameof(Spawn));
    }

    void EasySpawning()
    {
        
        GameObject plateformeEasy = PlateformPooling.SharedInstance.GetPooledPlateformeEasy(gameObject.transform.position);
        if(plateformeEasy!=null)
        {
            plateformeEasy.transform.position = spawningArea.transform.position;
            PlateformBehaviour plateformBehaviour = plateformeEasy.GetComponent<PlateformBehaviour>();
            plateformeEasy.SetActive(true);
            plateformBehaviour.currentSpeed = evolutionScript.currentSpeed;
            plateformBehaviour.currentBiome = Biome.Easy;

        }

    }
    
    void NormalSpawning()
    {
        GameObject plateformeNormal = PlateformPooling.SharedInstance.GetPooledPlateformeNormal(gameObject.transform.position);
        if(plateformeNormal!=null)
        {
            plateformeNormal.transform.position = spawningArea.transform.position;
            PlateformBehaviour plateformBehaviour = plateformeNormal.GetComponent<PlateformBehaviour>();
            plateformeNormal.SetActive(true);
            plateformBehaviour.currentSpeed = evolutionScript.currentSpeed;
            plateformBehaviour.currentBiome = Biome.Normal;

        }
    }

    void HardSpawning()
    {
        GameObject plateformeHard = PlateformPooling.SharedInstance.GetPooledPlateformeHard(gameObject.transform.position);
        if(plateformeHard!=null)
        {
            plateformeHard.transform.position = spawningArea.transform.position;
            PlateformBehaviour plateformBehaviour = plateformeHard.GetComponent<PlateformBehaviour>();
            plateformeHard.SetActive(true);
            plateformBehaviour.currentSpeed = evolutionScript.currentSpeed;
            plateformBehaviour.currentBiome = Biome.Hard;

        }
    }

}
