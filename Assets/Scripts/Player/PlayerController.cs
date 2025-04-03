using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Mouvement")]
    [SerializeField]
    private float moveSpeed = 4f;

    [Header("Saut")]
    [SerializeField]
    public int jumpPower;
    public float fallMultiplier;

    [Header("Réferences")]
    [SerializeField]
    private Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;


    private Vector2 moveDirection;
    private bool isFacingRight = true;
    public bool isGrounded;
    Vector2 vecGravity;
    public bool jump;

    private void Awake()
    {

        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);

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
        Jumping();
        
    }

    private void Jumping()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.43f, 0.08f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        if (jump && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpPower, 0);
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }
    }

    private void Move()
    {
        if (rb)
        {
            rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            Debug.LogError("Rigidbody2D is missing on PlayerController");
        }
    }

    public void Jump()
    {
        jump = true;
    }

    public void NoJump()
    {
        jump = false;
    }
}
