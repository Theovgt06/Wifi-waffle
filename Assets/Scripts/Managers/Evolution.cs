using UnityEngine;


public class Evolution : MonoBehaviour
{
    [SerializeField] private float easySpeed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float hardSpeed;
    [SerializeField] private float rationSpeedRate = 4.05f;

    public float spawnRate;
    public Biome currentBiome;
    public int spawnedPlatform;
    private int currentSpawnedPlatform = -1;
    public float currentSpeed;

    private int maxEasy = 50;
    private int maxNormal = 175;

    private float transitionEasyToNormal;
    private float transitionNormalToHard;
    private float ratioEasyToNormal;
    private float ratioNormalToHard;


    void Start()
    {
        currentBiome = Biome.Easy;
        currentSpeed = easySpeed;
        spawnRate = rationSpeedRate / currentSpeed;
        transitionEasyToNormal = normalSpeed - easySpeed;
        transitionNormalToHard = hardSpeed - normalSpeed;
        ratioEasyToNormal = transitionEasyToNormal / maxEasy;
        ratioNormalToHard = transitionNormalToHard / maxNormal;

    }

    void Update()
    {
        if (spawnedPlatform >= 336 && spawnedPlatform <= (maxEasy+336))
        {
            currentBiome = Biome.Easy;
            UpdateSpeed(ratioEasyToNormal);
            spawnRate = rationSpeedRate / currentSpeed;

        }
        else if (spawnedPlatform > (maxEasy+336) && spawnedPlatform <= (maxNormal+336))
        {
            currentBiome = Biome.Normal;
            UpdateSpeed(ratioNormalToHard);
            spawnRate = rationSpeedRate / currentSpeed;
        }
        else if (spawnedPlatform > (maxNormal+336))
        {
            currentBiome = Biome.Hard;
            currentSpeed = hardSpeed;
        }
    }

    void UpdateSpeed(float ratio)
    {
        if(spawnedPlatform>currentSpawnedPlatform)
        {
            currentSpeed+=ratio;
            currentSpawnedPlatform = spawnedPlatform;
        }
    }
}