using UnityEngine;
using DG.Tweening;

public class PlayerShooting : MonoBehaviour, WeaponBehaviour {
    // Références aux objets
    private GameObject mousePosition;
    private GameObject player;

    public GameObject playerBullet;
    [SerializeField] private float bulletSpeed = 20f;

    public int ammoAmmount = 9; 
    public bool canShoot = true;
    
    
    void Start()
    {
        // Idéalement, ces références devraient être configurées via l'inspecteur
        if (player == null) player = GameObject.Find("Player");
        if (mousePosition == null) mousePosition = GameObject.Find("Shoot Indicator");
    }
    
    void Shooting(Vector2 aimLocation, float speed)
    {
        // Créer le projectile à la position du joueur
        GameObject prefabBullet = Instantiate(playerBullet, player.transform.position, Quaternion.identity);
        Rigidbody2D prefabRB = prefabBullet.GetComponent<Rigidbody2D>();
        
        // Calculer les coordonnées au-delà de aimLocation (par exemple 2x plus loin de la position actuelle)
        Vector2 playerPos = player.transform.position;
        Vector2 extendedTarget = playerPos + ((aimLocation - playerPos) * 4f);
        
        // Déplacer avec DOTween
        float distance = Vector2.Distance(playerPos, extendedTarget);
        float duration = distance / bulletSpeed;
        prefabRB.DOMove(extendedTarget, duration);
    }
    
    public void Shoot()
    {
        // Obtenir la position actuelle de la cible au moment du tir
        Vector2 currentAimPosition = mousePosition.transform.position;
        if (ammoAmmount>0 && canShoot) // Si le Player possède assez de Munition et que le timing est bon.
        { 
            // Appel de Shooting
            Shooting(currentAimPosition, bulletSpeed);
        }
        
    }
}