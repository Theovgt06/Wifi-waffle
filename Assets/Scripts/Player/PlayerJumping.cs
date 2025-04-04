using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    [Header("Saut")]
    [SerializeField]
    public int jumpPower;
    public float fallMultiplier;

    [Header("Réferences")]
    [SerializeField]
    private Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public bool isGrounded;
    Vector2 vecGravity;
    public bool jump;

    private void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
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

    public void Jump()
    {
        jump = true;
    }

    public void NoJump()
    {
        jump = false;
    }
}
