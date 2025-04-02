using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Mouvement")]
    [SerializeField]
    private float moveSpeed = 4f;

    [Header("Saut")]
    [SerializeField]
    private float jumpPower;
    //private float fallMultiplier;

    [Header("Réferences")]
    [SerializeField]
    private Rigidbody2D rb;

    private Vector2 moveDirection;
    private bool isFacingRight = true;
    public bool jumping;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void Start()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.SetPlayerInput(playerInput);
            }
            else
            {
                Debug.LogError("InputManager is not in the scene");
            }
        }
        else
        {
            Debug.LogError("Missing PlayerInput on GameObject");
        }
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;

        if (direction.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && isFacingRight)
        {
            Flip();
        }
    }


    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }


    private void Move()
    {
        if (rb)
        {
            rb.linearVelocity = new Vector2(moveDirection.x, 0) * moveSpeed;
        }
        else
        {
            Debug.LogError("Rigidbody2D is missing on PlayerController");
        }
    }

    public void Jump()
    {
        if (jumping)
        {
            Debug.Log("test saut");
            rb.linearVelocity += new Vector2(rb.linearVelocity.x, jumpPower);
        }
    }


}
