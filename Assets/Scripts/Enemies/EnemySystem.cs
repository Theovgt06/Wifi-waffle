using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class EnemySystem : MonoBehaviour, IWeapons, IDamageable {
    // Références aux objets
    [SerializeField] private float shootDelay = 1f;
    private float lastShoot;
    private string enemyTag;
    [SerializeField]
    private BulletPooling bulletPooling;

    private GameObject player;


    void Awake()
    {
        enemyTag = gameObject.tag;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bulletPooling = gameObject.GetComponent<BulletPooling>();
    }

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
                ChaseExplode();
                break;
        }

        
        
    }
    public void ShootDirect()
    {
        if(CanShoot())
        {
            Vector2 playerPosition = player.transform.position;
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
            GameObject bullet = bulletPooling.GetPooledObject(gameObject.transform.position); 
            if (bullet == null) return;
            BulletBehaviour bulletBehaviourInstance = bullet.GetComponent<BulletBehaviour>();
            bulletBehaviourInstance.GetValues(gameObject, bullet,playerPosition, gameObject);
            bulletBehaviourInstance.SetBehaviourType(BulletBehaviour.BehaviourBullet.Parabolic);
        } 
    }
    
    
    public void ChaseExplode()
    {

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

    public void TakeDamage(int amount)
    {
        
    }
}