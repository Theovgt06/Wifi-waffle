using UnityEngine;
using DG.Tweening;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum BehaviourBullet { Inactive, Parabolic, Directional, Fixed }
    public BehaviourBullet currentBehaviour = BehaviourBullet.Inactive;
    private Vector2 aimPosition; 
    private GameObject instantiateBullet;
    private GameObject startPoint;
    private bool hasStartedDirectional = false;
    [SerializeField] private float shootDistance = 8f;
    [SerializeField] private float bulletSpeed = 8f;
    [SerializeField] public float peakDirectionXCalc;

    [SerializeField] public float peakDirectionYCalc;
    private GameObject fromWho;
    void Start()
    {

    } 

    public void GetValues(GameObject startingPoint, GameObject instantiateBulletRef, Vector2 aimingPosition, GameObject fromWhoRef)
    {
        startPoint = startingPoint;
        instantiateBullet = instantiateBulletRef;
        aimPosition = aimingPosition;
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
            case BehaviourBullet.Directional:
                HandleDirectional();
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
        if (hasStartedDirectional) return; // Condition stopping Behavior actualisation.
        hasStartedDirectional = true; 

        Vector2 startPos = startPoint.transform.position;
        // Set bullet Active and Shoot it.

        LaunchBezierParabola(startPos,aimPosition,instantiateBullet,peakDirectionXCalc,peakDirectionYCalc);
    }

    private void HandleDirectional()
    {
        if (hasStartedDirectional) return; // Condition stopping Behavior actualisation.
        hasStartedDirectional = true;  // ---> 


          // Get bullet components 
        Rigidbody2D bulletRB = instantiateBullet.GetComponent<Rigidbody2D>();
        Transform bulletTransform = instantiateBullet.GetComponent<Transform>();
      
        // Get direction and rotation to shoot.
        Vector2 startPos = startPoint.transform.position;
        Vector2 direction = (aimPosition - startPos).normalized; 

        // Compute Rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletTransform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        // Compute Movement 
        Vector2 extendedTarget = startPos + ((aimPosition - startPos)*shootDistance);  
        float distance = Vector2.Distance(startPos, extendedTarget);
        float duration = distance / bulletSpeed;
        
        // Set bullet Active and Shoot it.
        // instantiateBullet.SetActive(true);
        bulletRB.DOMove(extendedTarget, duration);
    }
    


    private void HandleFixed()
    {

    }

    void OnDisable()
    {
        // Annuler tous les tweens associés à cet objet quand il est désactivé
        DOTween.Kill(GetComponent<Rigidbody2D>());
        hasStartedDirectional = false; // Reset the flag
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject hitObject = other.gameObject;
        if(hitObject.name != fromWho.name)
        {
            if(hitObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                IDamageable target = hitObject.GetComponent<IDamageable>();
                target.TakeDamage(1);
                BulletBehaviour bulletBehavior = gameObject.GetComponent<BulletBehaviour>();
                bulletBehavior.SetBehaviourType(BulletBehaviour.BehaviourBullet.Inactive);
            }
            if(hitObject.CompareTag("Pink") || hitObject.CompareTag("Frog") | hitObject.CompareTag("Vodoo"))
            {
                gameObject.SetActive(false);
                IDamageable target = hitObject.GetComponent<IDamageable>();
                target.TakeDamage(1);
                BulletBehaviour bulletBehavior = gameObject.GetComponent<BulletBehaviour>();
                bulletBehavior.SetBehaviourType(BulletBehaviour.BehaviourBullet.Inactive);
            }
            if(hitObject.CompareTag("Edge Collider"))
            {
                gameObject.SetActive(false);
                BulletBehaviour bulletBehavior = gameObject.GetComponent<BulletBehaviour>();
                bulletBehavior.SetBehaviourType(BulletBehaviour.BehaviourBullet.Inactive);
                gameObject.transform.position = startPoint.transform.position;
            }

        }
    }

    public void LaunchBezierParabola(Vector3 startPos, Vector3 targetPos, GameObject bullet, float peakDirectionX, float peakDirectionY)
    {       
        bullet.SetActive(true);
        if(startPos.x>=targetPos.x){
            peakDirectionX *=-1;
        }
        Vector3 peakPos = startPos + Vector3.up * peakDirectionY + Vector3.right * peakDirectionX;
        float duration = 1f;

        Tween bezierTween = DOTween.To(t => {
            Vector3 p0 = startPos;
            Vector3 p1 = peakPos;
            Vector3 p2 = targetPos;

            float oneMinusT = 1f - t;
            Vector3 pos = oneMinusT * oneMinusT * p0 + 
                        2f * oneMinusT * t * p1 + 
                        t * t * p2;

            bullet.transform.position = pos;

        }, 0f, 1f, duration).SetEase(Ease.Linear);
    }
}
