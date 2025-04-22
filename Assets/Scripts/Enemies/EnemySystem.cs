using UnityEngine;
using DG.Tweening;
public class EnemySystem : MonoBehaviour, IWeapons, IDamageable {
    // Références aux objets

    
    private enum EnemyType {Frog, Pink, Vodoo}
    public enum VodooState { Chase, Explode, Routine, Inactive,Dead};
    [SerializeField] private float shootDelay = 1f;
    [SerializeField] private float hitPlayerDelay;
    [SerializeField] private float vodooSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float vodooChaseSpeed;
    [SerializeField] private float maxChaseSpeed;
    [SerializeReference] private bool isFacingRight = true;

    [SerializeField] private EnemyType enemyType;
    [SerializeField]private bool isGrounded;

    private float lastShoot;
    private float lastHitPlayer;
    [SerializeField]
    private BulletPooling bulletPooling;
    private GameObject player;
    
    public VodooState currentVodooState = VodooState.Inactive;
    private Transform groundCheck;
    public LayerMask groundLayer;
    private Animator anim;
    private Rigidbody2D rb;
    private DataChanger dataChanger;
    [SerializeField] private GameObject playerGroundCheck;
    [SerializeField] private GameObject currentVodooPlatform;
    [SerializeField] private GameObject currentPlayerPlatform;
    [SerializeField] private AudioManager audioManager;
   
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        playerGroundCheck = player.transform.GetChild(0).gameObject;
        bulletPooling = gameObject.GetComponent<BulletPooling>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dataChanger = GetComponent<DataChanger>();
        dataChanger.currentHealth = 1;
        if(enemyType == EnemyType.Vodoo)
        {
            groundCheck = gameObject.transform.GetChild(0).transform;
            isFacingRight = true;
        }

    }
    void FixedUpdate() 
    {
        
        switch (enemyType) 
        {
            case EnemyType.Frog:
                ShootDirect();
                break;
            case EnemyType.Pink:
                ShootParabolic();
                break;
            case EnemyType.Vodoo:
                switch(currentVodooState)
                    {
                    case VodooState.Inactive:
                        Inactive();
                        break;
                    case VodooState.Routine:
                        Routine();
                        break;
                    case VodooState.Chase:
                        Chase();
                        break;
                    case VodooState.Explode:
                        Explode();
                        break;
                    case VodooState.Dead:
                        Dead();
                        break;
                    }
                break;
        }  
    }


    void Update()
    {
        if(currentVodooState == VodooState.Routine){
            DetectPlayer();
        }
    }


    public void ShootDirect()
    {
        if(CanShoot())
        {
            Vector2 playerPosition = player.transform.position;
            if (playerPosition.x > transform.position.x && !isFacingRight) {
                Flip();
            }else if (playerPosition.x < transform.position.x && isFacingRight){
                Flip();
            }
            GameObject bullet = bulletPooling.GetPooledObject(gameObject.transform.position); 
            if (bullet == null) return;
            BulletBehaviour bulletBehaviourInstance = bullet.GetComponent<BulletBehaviour>();
            bulletBehaviourInstance.GetValues(gameObject, bullet,playerPosition, gameObject);
            bulletBehaviourInstance.SetBehaviourType(BulletBehaviour.BehaviourBullet.Directional);
            audioManager.PlaySfx(audioManager.frogDamaged);
            anim.SetTrigger("Attack");
        }
    }

    public void ShootParabolic()
    {
        if(CanShoot())
        {

            Vector2 playerPosition = player.transform.position;
            if (playerPosition.x > transform.position.x && !isFacingRight) {
                Flip();
            }else if (playerPosition.x < transform.position.x && isFacingRight){
                Flip();
            }
            GameObject bullet = bulletPooling.GetPooledObject(gameObject.transform.position); 
            if (bullet == null) return;
            BulletBehaviour bulletBehaviourInstance = bullet.GetComponent<BulletBehaviour>();
            bulletBehaviourInstance.GetValues(gameObject, bullet,playerPosition, gameObject);
            bulletBehaviourInstance.SetBehaviourType(BulletBehaviour.BehaviourBullet.Parabolic);
            audioManager.PlaySfx(audioManager.pinkShooting);
            anim.SetTrigger("Attack");
        } 
    }
     
    private bool CanShoot()
    {
        if (Time.time - lastShoot > shootDelay && player.transform.position.y<transform.position.y) // Comparaison avec le temps global
        {
            lastShoot = Time.time; // Mise à jour du dernier tir
            return true;
        }
        return false;
    }


    void Inactive(){
        if(rb.linearVelocity.y==0){
            currentVodooState = VodooState.Routine;
        }
    }

    public void Routine()
    {
        isGrounded = Physics2D.OverlapCapsule(
            groundCheck.position,
            new Vector2(0.43f, 0.08f),
            CapsuleDirection2D.Horizontal,
            0f,
            groundLayer
        );
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Plateform"))
        {
            currentVodooPlatform = hit.collider.gameObject;
        }
        if (isGrounded)
        {
            float direction = isFacingRight ? 1f : -1f;
            float newVelocityX = Mathf.Clamp(rb.linearVelocity.x + direction * vodooSpeed * Time.fixedDeltaTime, -maxSpeed, maxSpeed);
            rb.linearVelocity = new Vector2(newVelocityX, rb.linearVelocity.y);  
        }
        else
        {
            Flip();
        }
    }

    public void Chase()
    {
        isGrounded = Physics2D.OverlapCapsule(
            groundCheck.position,
            new Vector2(0.43f, 0.08f),
            CapsuleDirection2D.Horizontal,
            0f,
            groundLayer
        );
        if (isGrounded)
        {
            float direction = isFacingRight ? 1f : -1f;
            float newVelocityX = Mathf.Clamp(rb.linearVelocity.x + direction * vodooChaseSpeed * Time.fixedDeltaTime, -maxChaseSpeed, maxChaseSpeed);
            rb.linearVelocity = new Vector2(newVelocityX, rb.linearVelocity.y);  
        }
        if(!isGrounded){
            currentVodooState = VodooState.Explode;
        }

    }
    public void Explode()
    {
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        anim.SetTrigger("Die");        
        currentVodooState = VodooState.Dead;

    }

    public void Dead()
    {
    }


    public void TakeDamage(int amount)
    {
        dataChanger.ChangeHealth(amount);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        GetComponent<SpriteRenderer>().flipX = !isFacingRight;
        if(enemyType == EnemyType.Vodoo)
        {
            Vector3 groundPos = groundCheck.localPosition;
            if(isFacingRight){
            groundPos.x = 0.35f;
            }else{
                groundPos.x = -0.35f;
            }
            groundCheck.localPosition = groundPos;
        }
        
    }


    private void DetectPlayer()
    {
        currentPlayerPlatform = playerGroundCheck.GetComponent<ParentingPlatform>().currentPlatformOn;         
        if(currentVodooPlatform == currentPlayerPlatform && currentVodooPlatform!=null && currentPlayerPlatform!=null)
        {
            if((isFacingRight && transform.position.x<= player.transform.position.x) || (!isFacingRight && transform.position.x> player.transform.position.x))
            {
                currentVodooState = VodooState.Chase;
                audioManager.PlaySfx(audioManager.spotted);
            }
        }
    }

    private Vector2 KnockbackDirection(Vector2 playerPos, Vector2 enemyPos)
    {
        float direction = (playerPos.x>enemyPos.x) ? 1f : -1f;
        return new Vector2(playerPos.x+1f*direction,playerPos.y+0.5f*direction);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.gameObject.CompareTag("Player") || !(Time.time - lastHitPlayer > hitPlayerDelay))
        {
            return;
        }
        lastHitPlayer = Time.time;
        if(enemyType == EnemyType.Vodoo)
        {
            other.gameObject.GetComponent<PlayerSystem>().TakeDamage(-2);
            if(currentVodooState != VodooState.Explode)
            {
                currentVodooState = VodooState.Explode;
                audioManager.PlaySfx(audioManager.damageTaken);
                audioManager.PlaySfx(audioManager.voodooDamaged);
            }
            // other.gameObject.GetComponent<PlayerSystem>().rb.DOMove(KnockbackDirection(other.transform.position,transform.position),0.5f);
        }else{
            other.gameObject.GetComponent<PlayerSystem>().TakeDamage(-1);
            audioManager.PlaySfx(audioManager.damageTaken);
            // other.gameObject.GetComponent<PlayerSystem>().rb.DOMove(KnockbackDirection(other.transform.position,transform.position),0.5f);
        }
    }

}