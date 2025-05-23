using UnityEngine;
using DG.Tweening;

public class PlayerSystem : MonoBehaviour, IWeapons, IDamageable {
    // Références aux objets
    [SerializeField] private int ammoAmmount; 
    [SerializeField] private float shootDelay = 0.5f;
    [SerializeField] private BulletPooling bulletPooling;

    private float lastShoot;
    private GameObject shootIndicator;
    private DataChanger dataChanger;
    public Animator anim;
    public Rigidbody2D rb;
    [SerializeField] private AudioManager audioManager;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // Idéalement, ces références devraient être configurées via l'inspecteur
        shootIndicator = GameObject.FindGameObjectWithTag("ShootIndicator");
        bulletPooling = GetComponent<BulletPooling>();
        dataChanger = GetComponent<DataChanger>();

    }

    void Update() 
    {
        ammoAmmount = dataChanger.currentAmmo;
    }
    public void Shoot()
    {
        if(CanShoot())
        {
            Vector2 mousePosition = shootIndicator.transform.position;
            GameObject bullet = bulletPooling.GetPooledObject(gameObject.transform.position); 
            if (bullet == null) return;
            BulletBehaviour bulletBehaviourInstance = bullet.GetComponent<BulletBehaviour>();
            bulletBehaviourInstance.GetValues(gameObject, bullet,mousePosition, gameObject);
            bulletBehaviourInstance.SetBehaviourType(BulletBehaviour.BehaviourBullet.Directional);
            audioManager.PlaySfx(audioManager.shooting);
            dataChanger.ChangeAmmo(-1);
        } 
    }
    
    private bool CanShoot()
    {
        if (ammoAmmount>0 &&Time.time - lastShoot > shootDelay && ammoAmmount>0) // Comparaison avec le temps global
        {
            lastShoot = Time.time; // Mise à jour du dernier tir
            return true;
        }
        return false;
    }

    public void TakeDamage(int amount)
    {
        dataChanger.ChangeHealth(amount);
        audioManager.PlaySfx(audioManager.damageTaken);
        anim.SetTrigger("Hit");
    }
}