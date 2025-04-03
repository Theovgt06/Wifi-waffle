using System;
using UnityEngine;


public enum Biome {Stop, Easy, Normal, Hard, Expert};

public class PlateformSpawning : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [Header("Speed Plateforme Parametres ")]
    [Tooltip("Ratio de *10?")]
    [SerializeField]   
    private float easySpeed;
    private float normalSpeed;
    private float hardSpeed;
    private float expertSpeed;

    private GameObject spawningArea;
    private Biome currentBiome = Biome.Easy;

    [SerializeField] 
    private float spawnRate =  2f;
    void Start()
    {
        spawningArea = GameObject.FindGameObjectWithTag("Spawning Area");
    }
    private void OnEnable()
    {
        // Démarre le spawn avec un intervalle de `spawnRate` secondes.
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
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
            case Biome.Expert:
                ExpertSpawning();
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
            plateformBehaviour.ChangeSpeed(easySpeed);

        }

    }
    
    void NormalSpawning()
    {

    }

    void HardSpawning()
    {

    }
    
    void ExpertSpawning()
    {

    }
}
