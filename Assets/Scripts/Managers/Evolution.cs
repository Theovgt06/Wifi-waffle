using UnityEngine;

public class Evolution : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float easySpeed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float hardSpeed;
    [SerializeField] private float rationSpeedRate = 4.05f;
    public float spawnRate;
    public Biome currentBiome;
    public int spawnedPlatform;
    public float currentSpeed;
    
    void Start()
    {
        currentBiome = Biome.Easy;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedPlatform > 0 && spawnedPlatform <= 60)
        {
            // Niveau Easy
            currentBiome = Biome.Easy;
            currentSpeed = easySpeed;
            spawnRate = rationSpeedRate/currentSpeed;
        }
        else if (spawnedPlatform > 60 && spawnedPlatform <= 200)
        {
            // Niveau Normal
            currentBiome = Biome.Normal;
            currentSpeed = normalSpeed;
            spawnRate = rationSpeedRate/currentSpeed;
        }
        else if (spawnedPlatform > 200)
        {
            // Niveau Hard
            currentBiome = Biome.Hard;
            currentSpeed = hardSpeed;
            spawnRate = rationSpeedRate/currentSpeed;
        }
    }
}
