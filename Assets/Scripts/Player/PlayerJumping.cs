using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    [Header("Saut")]
    [SerializeField] public int jumpPower;
    [SerializeField] private float fallMultiplier;


    [Header("Références")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerMovement playerMovement;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public bool isGrounded;
    private bool jumpRequested;
    private bool isJumping;
    Vector2 vecGravity;
    private Animator anim;

    private void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (rb.linearVelocity.y > 0)
        {
            anim.SetBool("isJumping", true);
        }
        else if (rb.linearVelocity.y < 0)
        {
            anim.SetBool("isJumping", false);
        }
           anim.SetBool("isGrounded", isGrounded);

    }

    private void FixedUpdate()
    {
        HandleGroundDetection();  // Détecter si le joueur est au sol
        JumpingLogic();  // Appliquer la logique du saut
    }

    private void HandleGroundDetection()
    {
        // Détecter si le joueur est au sol (selon le groundCheck)
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.43f, 0.08f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    private void JumpingLogic()
    {
        // Si le joueur demande un saut et qu'il est au sol
        if (jumpRequested && isGrounded)
        {
            isJumping = true;  // Le joueur est en train de sauter
            anim.SetTrigger("Jump");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);  // Appliquer la vitesse du saut
            jumpRequested = false;  // Réinitialiser la demande de saut
        }

        // Gérer la vitesse de chute
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity -= vecGravity * fallMultiplier * Time.deltaTime;  // Accélérer la chute
        }

        // Si le joueur touche le sol, on désactive le saut en l'air
        if (isGrounded && rb.linearVelocity.y == 0)
        {
            isJumping = false;
        }
    }

    public void Jump()
    {
        if (!isJumping)  // Si le joueur n'est pas déjà en train de sauter
        {
            jumpRequested = true;  // Demander un saut
        }
    }

    public void NoJump()
    {
        jumpRequested = false;
    }
}
