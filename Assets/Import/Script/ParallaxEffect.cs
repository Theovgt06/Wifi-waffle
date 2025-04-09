using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Transform[] layers; // Array of all the layers to be parallaxed
    [SerializeField] private float[] parallaxScales; // The proportion of the camera's movement to move the layers by
    [SerializeField] private float smoothing = 1f; // How smooth the parallax effect should be

    private Transform cam; // Reference to the main camera's transform
    private Vector3 previousCamPos; // The position of the camera in the previous frame

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;

        // Assigning corresponding parallax scales if not set
        if (parallaxScales.Length != layers.Length)
        {
            parallaxScales = new float[layers.Length];
            for (int i = 0; i < layers.Length; i++)
            {
                parallaxScales[i] = layers[i].position.z * -1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            // The parallax is the opposite of the camera movement because the previous frame multiplied by the scale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // Set a target x position which is the current position plus the parallax
            float targetPosX = layers[i].position.x + parallax;

            // Create a target position which is the layer's current position with its target x position
            Vector3 targetPos = new Vector3(targetPosX, layers[i].position.y, layers[i].position.z);

            // Smoothly transition between the current position and the target position using Lerp
            layers[i].position = Vector3.Lerp(layers[i].position, targetPos, smoothing * Time.deltaTime);
        }

        // Set the previousCamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;
    }
}
