using UnityEngine;
using DG.Tweening;

public class PlayerSystem : MonoBehaviour, IWeapons, IDamageable {
    // Références aux objets
    [SerializeField] private int ammoAmmount = 3; 
    [SerializeField] private float shootDelay = 0.5f;
    [SerializeField] private BulletPooling bulletPooling;

    private float lastShoot;
    private GameObject shootIndicator;

    private DataChanger dataChanger;

    
    void Start()
    {
        // Idéalement, ces références devraient être configurées via l'inspecteur
        shootIndicator = GameObject.Find("Shoot Indicator");
        bulletPooling = gameObject.GetComponent<BulletPooling>();
        dataChanger = gameObject.GetComponent<DataChanger>();

    }

    void Update() 
    {

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
            dataChanger.ChangeAmmo(-1);
        } 
    }
    
    private bool CanShoot()
    {
        if (ammoAmmount>0 &&Time.time - lastShoot > shootDelay) // Comparaison avec le temps global
        {
            lastShoot = Time.time; // Mise à jour du dernier tir
            return true;
        }
        return false;
    }

    public void TakeDamage(int amount)
    {
        dataChanger.ChangeHealth(amount);

    }
}