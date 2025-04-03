using UnityEngine;

public class PlateformBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    enum BehaviorBiome { Easy, Normal, Hard }

    private PlatformEffector2D platformEffector;

    void Start()
    {
        platformEffector = GetComponent<PlatformEffector2D>();
        if (platformEffector != null)
        {
            platformEffector.useOneWay = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
