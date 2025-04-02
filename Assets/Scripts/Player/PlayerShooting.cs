using UnityEngine;
using DG.Tweening;

public class PlayerShooting : MonoBehaviour, IWeapons {
    // Références aux objets
    [SerializeField] private int ammoAmmount = 3; 
    [SerializeField] private float shootDelay = 0.5f;
    private float lastShoot;
    private GameObject shootIndicator;
    
    void Start()
    {
        // Idéalement, ces références devraient être configurées via l'inspecteur
        shootIndicator = GameObject.Find("Shoot Indicator");
    }

    void Update() 
    {

    }
    public void Shoot()
    {
        if(canShoot())
        {
            Vector2 mousePosition = shootIndicator.transform.position;
            GameObject bullet = BulletPooling.SharedInstance.GetPooledObject(); 
            if (bullet == null) return;
            bullet.SetActive(true);
            BulletBehaviour bulletBehaviourInstance = bullet.GetComponent<BulletBehaviour>();
            bulletBehaviourInstance.GetValues(gameObject, bullet,mousePosition, gameObject);
            bulletBehaviourInstance.SetBehaviourType(BulletBehaviour.BehaviourBullet.Directional);
            // ammoAmmount-=1;
        } 
    }
    
    private bool canShoot()
    {
        if (ammoAmmount>0 &&Time.time - lastShoot > shootDelay) // Comparaison avec le temps global
        {
            lastShoot = Time.time; // Mise à jour du dernier tir
            return true;
        }
        return false;
    }

    public void TakeDamage()
    {

    }
}