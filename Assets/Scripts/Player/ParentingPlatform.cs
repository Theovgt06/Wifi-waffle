using UnityEngine;
using UnityEngine.Animations;
using System.Collections.Generic;



public class ParentingPlatform : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    public LayerMask groundLayer;

    // Capsule size & direction (utilisés aussi pour le gizmo)
    private GameObject player;
    // Référence au Rigidbody2D
    private Rigidbody2D rb;

    // Référence à la plateforme actuelle
    private Transform currentPlatform;
    private PlayerJumping playerJumping;
    private Vector3 previousPlatformPosition;


    void Start()
    {
        rb = gameObject.GetComponentInParent<Rigidbody2D>();
        player = transform.parent.gameObject;
        playerJumping = player.GetComponent<PlayerJumping>();
    }


    void OnTriggerStay2D(Collider2D other)
    {
        GameObject touchedObject = other.gameObject;

        if (touchedObject.CompareTag("Plateform"))
        {
            float platformTop = touchedObject.transform.position.y + touchedObject.GetComponent<Collider2D>().bounds.extents.y-14.5f;
            float playerBottom = player.transform.position.y - player.GetComponent<Collider2D>().bounds.extents.y;
            float yDifference = playerBottom - platformTop;
            Debug.Log(yDifference);
            // Si le joueur est au-dessus de la plateforme et qu'il descend
            if (yDifference > 0 && rb.linearVelocity.y <= 0.05f)
            {
                player.transform.SetParent(touchedObject.transform);
                rb.interpolation = RigidbodyInterpolation2D.None;
            }      
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (player.transform.parent != null && other.gameObject.CompareTag("Plateform"))
        {
            player.transform.SetParent(null);
            currentPlatform = null;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        }
    }
}