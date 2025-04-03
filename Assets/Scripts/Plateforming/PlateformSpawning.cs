using System;
using UnityEngine;

public class PlateformSpawning : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private enum Biome {Stop, Easy, Normal, Hard, Expert};
    private Biome currentBiome = Biome.Easy;

    [Header("Speed Plateforme Parametres ")]
    [SerializeField]
    private float easySpeed;
    private float normalSpeed;
    private float hardSpeed;
    private float expertSpeed;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        
    }

    void EasySpawning()
    {
        
        GameObject plateformeEasy = PlateformPooling.SharedInstance.GetPooledPlateformeEasy(gameObject.transform.position);
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
