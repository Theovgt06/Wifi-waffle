using UnityEngine;
using DG.Tweening;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum BehaviourBullet { Inactive, Parabolic, DirectionalPlayer , DirectionalEnemy, Fixed }
    public BehaviourBullet currentBehaviour = BehaviourBullet.Inactive;
    private Vector2 mousePosition; 
    private GameObject instantiateBullet;
    private GameObject player;
    private bool hasStartedDirectional = false;
    [SerializeField] private float shootDistance = 8f;
    [SerializeField] private float bulletSpeed = 8f;
    private GameObject fromWho;
    void Start()
    {

    } 

    public void GetValues(GameObject playerRef, GameObject instantiateBulletRef, Vector2 mousePositionRef, GameObject fromWhoRef)
    {
        player = playerRef;
        instantiateBullet = instantiateBulletRef;
        mousePosition = mousePositionRef;
        fromWho = fromWhoRef;
    }
    // Update is called once per frame

    public void SetBehaviourType(BehaviourBullet behavior)
    {
        currentBehaviour = behavior;
        hasStartedDirectional = false;
    }
    void FixedUpdate()
    {
        switch(currentBehaviour){
            case BehaviourBullet.Inactive:
                HandleInactive();
                break;
            case BehaviourBullet.Parabolic:
                HandleParabolic();
                break;
            case BehaviourBullet.DirectionalPlayer:
                HandleDirectionalPlayer();
                break;
            case BehaviourBullet.DirectionalEnemy:
                HandleDirectionalEnemy();
                break;
            case BehaviourBullet.Fixed:
                HandleFixed();
                break;
        }
    }

    private void HandleInactive() 
    {
        
    }

    private void HandleParabolic()
    {
        
    }

    private void HandleDirectionalPlayer()
    {
        if (hasStartedDirectional) return; // Condition stopping Behavior actualisation.
        hasStartedDirectional = true;  // ---> 


          // Get bullet components 
        Rigidbody2D bulletRB = instantiateBullet.GetComponent<Rigidbody2D>();
        Transform bulletTransform = instantiateBullet.GetComponent<Transform>();
      
        // Get direction and rotation to shoot.
        Vector2 playerPos = player.transform.position;
        Vector2 direction = (mousePosition - playerPos).normalized; 

        // Compute Rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletTransform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);

        // Compute Movement 
        Vector2 extendedTarget = playerPos + ((mousePosition - playerPos)*shootDistance);  
        float distance = Vector2.Distance(playerPos, extendedTarget);
        float duration = distance / bulletSpeed;
        
        // Set bullet Active and Shoot it.
        instantiateBullet.SetActive(true);
        bulletRB.DOMove(extendedTarget, duration);
    }


    private void HandleDirectionalEnemy()
    {

    }
    

    
    private void HandleFixed()
    {

    }

    void OnDisable()
    {
        // Annuler tous les tweens associés à cet objet quand il est désactivé
        DOTween.Kill(GetComponent<Rigidbody2D>());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject hitObject = other.gameObject;
        if(hitObject.name != fromWho.name)
        {
            if(hitObject.CompareTag("Player"))
            {
                IDamageable target = hitObject.GetComponent<IDamageable>();
                target.TakeDamage(1);
                gameObject.SetActive(false);
            }
            if(hitObject.CompareTag("Enemy"))
            {
                IDamageable target = hitObject.GetComponent<IDamageable>();
                target.TakeDamage(1);
                gameObject.SetActive(false);
            }
            if(hitObject.CompareTag("Edge Collider"))
            {
                gameObject.SetActive(false);
                gameObject.transform.position = player.transform.position;
            }

        }
    }

    
}
