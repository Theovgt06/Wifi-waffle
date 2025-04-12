using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class EnemySystem : MonoBehaviour, IWeapons, IDamageable {
    // Références aux objets

    
    private enum EnemyType {Frog, Pink, Vodoo}
    public enum VodooState { Chase, Explode, Routine};
    [SerializeField] private float shootDelay = 1f;
    [SerializeField] private float vodooSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private EnemyType enemyType;

    private float lastShoot;
    [SerializeField]
    private BulletPooling bulletPooling;
    private GameObject player;
    
    public VodooState currentVodooState = VodooState.Routine;
    private Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    private bool isFacingRight = true;
    private Animator anim;
    private Rigidbody2D rb;
    private DataChanger dataChanger;
   
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bulletPooling = gameObject.GetComponent<BulletPooling>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dataChanger = GetComponent<DataChanger>();
        if(enemyType == EnemyType.Vodoo)
        {
            groundCheck = gameObject.transform.GetChild(0).transform;
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
                    case VodooState.Routine:
                        Routine();
                        break;
                    case VodooState.Chase:
                        Chase();
                        break;
                    case VodooState.Explode:
                        Explode();
                        break;
                    }
                break;
        }  
    }


    void Update()
    {

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
            anim.SetTrigger("Attack");
        } 
    }
     
    private bool CanShoot()
    {
        if (Time.time - lastShoot > shootDelay) // Comparaison avec le temps global
        {
            lastShoot = Time.time; // Mise à jour du dernier tir
            return true;
        }
        return false;
    }

    public void Routine()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.43f, 0.08f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        if(isGrounded){
            float direction = isFacingRight ? 1f : -1f;
            float newVelocityX = Mathf.Clamp(rb.linearVelocity.x + direction * vodooSpeed * Time.fixedDeltaTime, -maxSpeed, maxSpeed);
            rb.linearVelocity = new Vector2(newVelocityX, rb.linearVelocity.y);            
        }else{
            Flip();
        }
    }

    public void Chase()
    {

    }
    public void Explode()
    {

    }



    public void TakeDamage(int amount)
    {
        dataChanger.ChangeHealth(amount);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    private void DetectPlayer()
    {
        if(gameObject.transform.parent.gameObject.transform.parent)
        {
            
        }
    }
}