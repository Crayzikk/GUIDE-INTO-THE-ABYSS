using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    // Enemy state
    protected abstract int health { get; set; } 
    protected abstract int damage { get; set; }
    protected abstract float attackRate { get; set; }
    private float nextTimeToAttack = 0f;

    // Event
    // public delegate void EnemyDead(); 
    // public event EnemyDead OnEnemyDead;

    // Components
    private Health healthEnemyController;
    private NavMeshAgent enemyNavMeshAgent;
    private Animator animatorEnemy;
    protected EnemyAttackTriger enemyAttackTriger;

    // Other
    private float radiusTrigger = 30f;
    protected Transform currentTrigger;

    // Cheking
    private bool enemyDie;
    protected bool enemyAttack;
    private bool enemyHit;

    // Animation
    private float animationTime;
    private float targetTime = 0.10f;
    private bool eventTrigger;

    [SerializeField] private AudioClip runClip;
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip takeDamageClip;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        healthEnemyController = GetComponent<Health>();
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        animatorEnemy = GetComponent<Animator>();
        enemyAttackTriger = GetComponentInChildren<EnemyAttackTriger>();

        enemyAttackTriger.SetDamageAttack(damage);
        healthEnemyController.InitializeHealth(health);

        enemyDie = false;
        enemyAttack = false;
    }

    protected void Update()
    {
        AnimatorStateInfo stateInfo = animatorEnemy.GetCurrentAnimatorStateInfo(0);

        if(!healthEnemyController.healthInZero)
        {   
            healthEnemyController.DebugHealh();

            if(currentTrigger == null)
                TriggerEnemy();
            
            if(currentTrigger != null)
                Move(currentTrigger.position);
            enemyNavMeshAgent.isStopped = true;

            if(!enemyAttack && !enemyHit && currentTrigger != null)
            {
                enemyNavMeshAgent.isStopped = false;
                Move(currentTrigger.position);
            }

            if(enemyHit)
            {
                if(stateInfo.normalizedTime >= 0.9f)
                {
                    enemyHit = false;
                    enemyNavMeshAgent.isStopped = false;
                }
            }
            else
            {
                if(currentTrigger != null)
                {
                    if(HasEnemyReachedTarget())
                    {
                        if(Time.time >= nextTimeToAttack)
                        {
                            nextTimeToAttack = Time.time + attackRate;
                            Attack();
                        }
                        else
                        {
                            if(stateInfo.IsName("attack1"))
                            {
                                animationTime = stateInfo.normalizedTime % 1 * stateInfo.length;
                                
                                if(animationTime >= targetTime && !eventTrigger)
                                {
                                    eventTrigger = true;
                                    enemyAttackTriger.enemyStartAttack = true;
                                }
                            }

                            if(stateInfo.normalizedTime >= 0.9f)
                            {
                                Vector3 directionToTarget = currentTrigger.position - transform.position;
                                Quaternion rotation = Quaternion.LookRotation(directionToTarget);
                                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);

                                animatorEnemy.Play("idle");
                                eventTrigger = false;
                            }
                        }
                    }
                    else
                    {
                        enemyAttack = false;
                    }
                }
            }

            animatorEnemy.SetBool("IsRunning", !enemyAttack);
        }
        else
        {
            if(enemyDie)
            {
                if(stateInfo.normalizedTime >= 0.9f)
                    Destroy(gameObject);
            }
            else
            {
                Die();
            }
        }
    }

    protected void Attack()
    {
        audioSource.clip = attackClip;
        audioSource.Play();
        enemyAttack = true;
        animatorEnemy.Play("attack1");
    }

    protected virtual void Die()
    {
        gameObject.layer = 0;
        animatorEnemy.Play("death");
        enemyDie = true;
    }
    
    protected void Move(Vector3 point)
    {
        if(currentTrigger != null)
        {
            enemyNavMeshAgent.SetDestination(point);
            audioSource.clip = runClip;
            audioSource.Play();
        }
            
    }

    protected void TriggerEnemy()
    {
        Collider[] collidersCharacter = Physics.OverlapSphere(transform.position, radiusTrigger, LayerMask.GetMask("Character"));

        if(collidersCharacter.Length > 0)
        {
            currentTrigger = collidersCharacter[Random.Range(0, collidersCharacter.Length - 1)].GetComponent<Transform>();
        }
        else
        {
            currentTrigger = null;
        }
    }

    protected bool HasEnemyReachedTarget()
    {
        if (enemyNavMeshAgent.remainingDistance <= enemyNavMeshAgent.stoppingDistance)
        {
            if (!enemyNavMeshAgent.hasPath || enemyNavMeshAgent.velocity.sqrMagnitude == 0f)
            {
                Debug.Log("Enemy досяг цілі!");
                return true;
            }
        }

        return false;
    }

    public void EnemyTakeDamage(int damage)
    {
        audioSource.clip = takeDamageClip;
        audioSource.Play();
        animatorEnemy.Play("hit_1");
        enemyNavMeshAgent.isStopped = true;
        enemyHit = true;
        healthEnemyController.TakeDamage(damage);
    }
}
