using UnityEngine;
using DG.Tweening;
using System;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum BehaviourBullet { Inactive, Parabolic, Directional, Fixed }
    public BehaviourBullet currentBehaviour = BehaviourBullet.Inactive;
    private Vector2 aimPosition; 
    private GameObject instantiateBullet;
    private GameObject startPoint;
    private bool hasStartedMoving = false;
    [SerializeField] private float bulletSpeed = 8f;
    [SerializeField] public float peakDirectionXCalc;
    
    [SerializeField] public float peakDirectionYCalc;
    private GameObject fromWho;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    void OnEnable()
    {   
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
        hasStartedMoving = false;
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
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void HandleParabolic()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        if (hasStartedMoving) return; // Condition stopping Behavior actualisation.
        hasStartedMoving = true; 

        Vector2 startPos = fromWho.transform.position;
        LaunchBezierParabola(startPos,aimPosition,instantiateBullet);

    }

    private void HandleDirectional()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        if (hasStartedMoving) return; // Condition stopping Behavior actualisation.
        hasStartedMoving = true;  // ---> 


          // Get bullet components 
        Rigidbody2D bulletRB = instantiateBullet.GetComponent<Rigidbody2D>();
        Transform bulletTransform = instantiateBullet.GetComponent<Transform>();
      
        // Get direction and rotation to shoot.
        Vector2 startPos = startPoint.transform.position;
        Vector2 direction = (aimPosition - startPos).normalized; 

        // Compute Rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletTransform.rotation = Quaternion.AngleAxis(angle-180, Vector3.forward);
        // Compute Movement 
        Vector2 extendedTarget = startPos + ((aimPosition - startPos)*int.MaxValue);  
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
        hasStartedMoving = false; // Reset the flag
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject hitObject = other.gameObject;
        if(hitObject.name != fromWho.name)
        {
            if(hitObject.CompareTag("Player"))
            {
                BulletBehaviour bulletBehavior = gameObject.GetComponent<BulletBehaviour>();
                bulletBehavior.SetBehaviourType(BehaviourBullet.Inactive);
                animator.SetTrigger("Explode");
                IDamageable target = hitObject.GetComponent<IDamageable>();
                audioManager.PlaySfx(audioManager.damageTaken);
                target.TakeDamage(-1);

            }
            if(hitObject.CompareTag("Pink"))
            {
                IDamageable target = hitObject.GetComponent<IDamageable>();
                target.TakeDamage(-1);
                BulletBehaviour bulletBehavior = gameObject.GetComponent<BulletBehaviour>();
                audioManager.PlaySfx(audioManager.pinkDamaged);
                animator.SetTrigger("Explode");
                Debug.Log("test");
                bulletBehavior.SetBehaviourType(BehaviourBullet.Inactive);
            }
            if (hitObject.CompareTag("Frog"))
            {
                IDamageable target = hitObject.GetComponent<IDamageable>();
                target.TakeDamage(-1);
                BulletBehaviour bulletBehavior = gameObject.GetComponent<BulletBehaviour>();
                audioManager.PlaySfx(audioManager.frogDamaged);
                animator.SetTrigger("Explode");
                Debug.Log("test");
                bulletBehavior.SetBehaviourType(BehaviourBullet.Inactive);
            }
            if(hitObject.CompareTag("Vodoo"))
            {
                IDamageable target = hitObject.GetComponent<IDamageable>();
                target.TakeDamage(-1);
                BulletBehaviour bulletBehavior = gameObject.GetComponent<BulletBehaviour>();
                audioManager.PlaySfx(audioManager.voodooDamaged);
                animator.SetTrigger("Explode");
                Debug.Log("test");
                bulletBehavior.SetBehaviourType(BehaviourBullet.Inactive);
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

    public void LaunchBezierParabola(Vector2 startPos, Vector3 targetPos, GameObject bullet)
    {       
        bullet.SetActive(true);
        float distance = Vector2.Distance(startPos, aimPosition);
        float heightFactor = 0.5f;
        Vector2 targetPosLose = new Vector2(targetPos.x, targetPos.y-30f);
        Vector2 peakPos = new Vector2(
            (startPos.x + aimPosition.x) / 2,
            Mathf.Max(startPos.y, aimPosition.y) + (distance * heightFactor));        
        float duration = 1f;    

        Tween bezierTween = DOTween.To(t => {
            Vector2 p0 = startPos;
            Vector2 p1 = peakPos;
            Vector2 p2 = targetPos;

            float oneMinusT = 1f - t;
            Vector2 pos = Mathf.Pow(oneMinusT,2)*p0 + 2*oneMinusT*t*p1
                        + Mathf.Pow(t,2)*p2;
            rb.MovePosition(pos);

        }, 0f, 1f, duration).SetEase(Ease.Linear)
        .OnComplete(() => {
        animator.SetTrigger("Explode"); // petite chute finale
        });

    }

    public void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
}
