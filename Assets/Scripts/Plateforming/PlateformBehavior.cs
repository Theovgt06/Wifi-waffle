using UnityEngine;

public class PlateformBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float currentSpeed;
    public int currentIndex;
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
        
        transform.position -= Vector3.up * currentSpeed * Time.deltaTime;  
        // if (transform.position.x <leftEdge){
        //     Vector3 newPosition = transform.position;
        //     newPosition.x += rightEdge;
        //     transform.position = newPosition;
        // }
    }

    public void ChangeSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
    }
}
