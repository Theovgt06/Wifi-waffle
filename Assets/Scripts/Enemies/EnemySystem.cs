using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class EnemySystem : MonoBehaviour, IWeapons, IDamageable {
    // Références aux objets

    public enum VodooState { Chase, Explode, Routine};
    [SerializeField] private float shootDelay = 1f;
    private float lastShoot;
    private string enemyTag;
    [SerializeField]
    private BulletPooling bulletPooling;

    private GameObject player;

    public VodooState currentVodooState = VodooState.Routine;

    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    private bool isFacingRight = true;
   
    
    void Awake()
    {
        enemyTag = gameObject.tag;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bulletPooling = gameObject.GetComponent<BulletPooling>();
    }


    // void GetGroundCheck;
    void Update() 
    {
        
        switch (enemyTag) 
        {
            case "Frog":
                ShootDirect();
                break;
            case "Pink":
                ShootParabolic();
                break;
            case "Vodoo":
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
    }

    public void Chase()
    {

    }
    public void Explode()
    {

    }



    public void TakeDamage(int amount)
    {
        
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}