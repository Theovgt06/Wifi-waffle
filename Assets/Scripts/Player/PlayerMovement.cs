using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Mouvement")]
    [SerializeField]
    public float moveSpeed = 4f;

    [Header("Réferences")]
    [SerializeField]
    private Rigidbody2D rb;
    public GameObject anchorRight;
    public GameObject anchorLeft;
    public Transform playerTransform;

    private Vector2 moveDirection;
    private bool isFacingRight = true;
    private Animator anim;
    

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        anim = GetComponent<Animator>();
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

    private void Update()
    {
        anim.SetFloat("Move", moveDirection.magnitude);

        if (anchorLeft.transform.position.x > playerTransform.position.x)
        {
            playerTransform.position = new Vector2((float)(anchorRight.transform.position.x-0.01), playerTransform.position.y);
        }
        if(anchorRight.transform.position.x < playerTransform.position.x)
        {
            playerTransform.position = new Vector2((float)(anchorLeft.transform.position.x+0.01), playerTransform.position.y);
        }
    }

    private void FixedUpdate()
    {
        Move();
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
}
