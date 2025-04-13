using UnityEngine;
using UnityEngine.Animations;
using System.Collections.Generic;



public class ParentingPlatform : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    public LayerMask groundLayer;
    private GameObject player;
    private Rigidbody2D rb;
    private PlayerJumping playerJumping;
    private Vector3 previousPlatformPosition;
    public GameObject currentPlatformOn;

    void Start()
    {
        rb = gameObject.GetComponentInParent<Rigidbody2D>();
        player = transform.parent.gameObject;
        playerJumping = player.GetComponent<PlayerJumping>();
    }


    void OnTriggerStay2D(Collider2D other)
    {
        currentPlatformOn = other.gameObject;

        if (currentPlatformOn.CompareTag("Plateform"))
        {
            float platformTop = currentPlatformOn.transform.position.y + currentPlatformOn.GetComponent<Collider2D>().bounds.extents.y-14.5f;
            float playerBottom = player.transform.position.y - player.GetComponent<Collider2D>().bounds.extents.y;
            float yDifference = playerBottom - platformTop;
            // Si le joueur est au-dessus de la plateforme et qu'il descend
            if (yDifference > 0 && rb.linearVelocity.y <= 0.05f)
            {
                player.transform.SetParent(currentPlatformOn.transform);
                rb.interpolation = RigidbodyInterpolation2D.None;
            }      
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (player.transform.parent != null && other.gameObject.CompareTag("Plateform"))
        {
            player.transform.SetParent(null);
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            currentPlatformOn = null;

        }
    }
}