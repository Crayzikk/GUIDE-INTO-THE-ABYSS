using UnityEngine;
using UnityEngine.AI;

public abstract class AICharacter : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] protected Transform[] targetPoints;
    [SerializeField] protected LayerMask layerMaskEnemy;

    // Components
    protected NavMeshAgent navMeshAgentCharacter;
    private Animator animator;
    
    // Other
    protected int indexTargetPoint;
    protected float radiusHasDetectEnemy = 10f;
    protected float radiusAttackEnemy = 10f;
    private RaycastHit agentHit;

    // Weapon
    [SerializeField] private Transform pointFire;
    private float shootingRange = 50;
    private int damageAgent = 40;

    // Enemy Detected
    protected bool agentDetectedEnemy;
    protected Vector3 pointEnemy;
    protected Enemy currentDetectedEnemy;

    // Particle System
    [SerializeField] private ParticleSystem muzzleFlash;

    // Time 
    protected float fireRate = 0.7f;
    protected float nextTimeToFire;


    void Start()
    {
        navMeshAgentCharacter = GetComponent<NavMeshAgent>();
        indexTargetPoint = 0;
    }

    void Update()
    {
        if(HasDetectedEnemy())
        {
            navMeshAgentCharacter.velocity = Vector3.zero;
            AgentAttackEnemy();
        }
        else
        {
            MoveToPoint(targetPoints[indexTargetPoint].position);

            if(HasAgentReachedTarget())
            {
                if(indexTargetPoint < targetPoints.Length)
                    indexTargetPoint++;
                
                MoveToPoint(targetPoints[indexTargetPoint].position);                
            }
        }
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red; // Встановлення кольору Gizmos
    //     Gizmos.DrawWireSphere(transform.position, radiusHasDetectEnemy); // Малювання проволочної сфери
    // }

    protected virtual void MoveToPoint(Vector3 point)
    {
        navMeshAgentCharacter.SetDestination(point);   
    }

    protected bool HasAgentReachedTarget()
    {
        if (!navMeshAgentCharacter.pathPending && navMeshAgentCharacter.remainingDistance <= navMeshAgentCharacter.stoppingDistance)
        {
            if (!navMeshAgentCharacter.hasPath || navMeshAgentCharacter.velocity.sqrMagnitude == 0f)
            {
                Debug.Log("Агент досяг цілі!");
                return true;
            }
        }

        return false;
    }
    
    private bool HasDetectedEnemy()
    {
        Collider[] collidersEnemy = Physics.OverlapSphere(transform.position, radiusHasDetectEnemy, LayerMask.GetMask("Enemy"));

        if(collidersEnemy.Length > 0)
        {
            agentDetectedEnemy = true;
            currentDetectedEnemy = collidersEnemy.Length > 1 
                ? collidersEnemy[Random.Range(0, collidersEnemy.Length - 1)].GetComponent<Enemy>()
                : collidersEnemy[0].GetComponent<Enemy>();
            
            return true;
        }

        return false;
    }

    private void AgentAttackEnemy()
    {
        Vector3 enemyPosition = currentDetectedEnemy.gameObject.transform.position;

        if(Vector3.Distance(transform.position, enemyPosition) <= radiusAttackEnemy)
        { 
            Vector3 directionToEnemy = enemyPosition - transform.position; 
            Quaternion rotation = Quaternion.LookRotation(directionToEnemy); 
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);

            if(Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + fireRate;
                AgentShoot();
            }
        }
    }

    private void AgentShoot()
    {
        muzzleFlash.Play();

        if (Physics.Raycast(pointFire.position, pointFire.forward, out agentHit, shootingRange))
        {
            agentHit.collider.GetComponent<Health>()?.TakeDamage(damageAgent);       
        }
    }

}
