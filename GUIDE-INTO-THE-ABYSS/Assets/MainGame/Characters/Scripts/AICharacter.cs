using UnityEngine;
using UnityEngine.AI;

public abstract class AICharacter : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] protected Transform[] targetPoints;
    [SerializeField] protected LayerMask layerMaskEnemy;

    // Components
    protected NavMeshAgent navMeshAgentCharacter;
    protected Animator animatorAgent;
    [SerializeField] Animator animatorAgentShortgun;
    
    // Other
    public int indexTargetPoint;
    protected float radiusHasDetectEnemy = 10f;
    protected float radiusAttackEnemy = 10f;
    private RaycastHit agentHit;

    // Weapon
    [SerializeField] private GameObject weaponAgent;
    [SerializeField] private Transform pointFire;
    private float shootingRange = 100;
    private int damageAgent = 40;

    // Enemy Detected
    protected bool agentDetectedEnemy;
    protected Vector3 pointEnemy;
    protected Vector3 currentDetectedEnemy;

    // Particle System
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem blood;

    // Time 
    protected float fireRate = 1.5f;
    protected float nextTimeToFire;

    // Audio
    [Header("Audio")]
    [SerializeField] protected AudioSource audioSourceShortGun;
    [SerializeField] protected AudioClip audioClipShoot;

    public bool runNextPoint;
    private bool characterDie;

    void Start()
    {
        animatorAgent = GetComponent<Animator>();
        navMeshAgentCharacter = GetComponent<NavMeshAgent>();
        indexTargetPoint = 0;

        audioSourceShortGun.clip = audioClipShoot;

        runNextPoint = true;
    }

    void Update()
    {
        
        if(!characterDie)
        {
            if(HasDetectedEnemy())
            {
                animatorAgentShortgun.SetBool("AgentShooting", true);
                
                animatorAgent.SetBool("IsShooting", true);
                navMeshAgentCharacter.velocity = Vector3.zero;

                AgentAttackEnemy();
            }
            else if(!PlotManager.characterStop)
            {
                navMeshAgentCharacter.isStopped = false;
                animatorAgentShortgun.SetBool("AgentShooting", false);

                animatorAgent.SetBool("IsShooting", false);
                animatorAgent.SetBool("IsRunning", runNextPoint);

                if(runNextPoint)
                    MoveToPoint(targetPoints[indexTargetPoint].position);

                if(HasAgentReachedTarget())
                {
                    if(indexTargetPoint < targetPoints.Length - 1)
                        indexTargetPoint++;
                    
                    MoveToPoint(targetPoints[indexTargetPoint].position);                
                }
            }
            else
            {
                animatorAgent.SetBool("IsShooting", false);
                animatorAgent.SetBool("IsRunning", false);
                navMeshAgentCharacter.isStopped = true;
            }
        }
        else
        {
            if(weaponAgent != null)
                Destroy(weaponAgent);
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
    
    protected virtual bool HasDetectedEnemy()
    {
        Collider[] collidersEnemy = Physics.OverlapSphere(transform.position, radiusHasDetectEnemy, layerMaskEnemy);
        return SetDetectedEnemy(collidersEnemy);
    }

    protected bool SetDetectedEnemy(Collider[] collidersEnemy)
    {
        if(collidersEnemy.Length > 0)
        {
            agentDetectedEnemy = true;
            currentDetectedEnemy = collidersEnemy.Length > 1 
            ? collidersEnemy[Random.Range(0, collidersEnemy.Length - 1)].bounds.center
            : collidersEnemy[0].bounds.center;
            
            return true;
        }

        return false;
    }

    private void AgentAttackEnemy()
    {
        if (Vector3.Distance(transform.position, currentDetectedEnemy) <= radiusAttackEnemy)
        {
            Vector3 directionToEnemy = currentDetectedEnemy - transform.position;

            Quaternion rotation = Quaternion.LookRotation(directionToEnemy);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 8f);

            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + fireRate;
                AgentShoot();
            }
        }
    }

    private void AgentShoot()
    {
        Vector3 directionToEnemy = currentDetectedEnemy - pointFire.position;

        // Debug.DrawRay(pointFire.position, directionToEnemy.normalized * shootingRange, Color.red, 2f);
        muzzleFlash.Play();
        audioSourceShortGun.Play();
        
        if (Physics.Raycast(pointFire.position, directionToEnemy.normalized, out RaycastHit hit, shootingRange, LayerMask.GetMask("Enemy")))
        {    
            ParticleSystem particleSystem = Instantiate(blood, hit.point, Quaternion.identity);
            Destroy(particleSystem, 3f);

            hit.collider.GetComponent<Enemy>()?.EnemyTakeDamage(damageAgent);
        }
    }

    public void Die()
    {
        characterDie = true;
        animatorAgent.Play("Dying Backwards");
    }

}
