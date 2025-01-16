using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Enemy state
    [SerializeField] protected int health; 
    [SerializeField] protected float speed; 
    [SerializeField] protected int damage; 

    // Event
    public delegate void EnemyDead(); 
    public event EnemyDead OnEnemyDead;

    // Other
    private Health healthEnemyController;

    void Start()
    {
        healthEnemyController = GetComponent<Health>();
    }

    void Update()
    {
        if(healthEnemyController.healthInZero)
            Die();
    }

    public abstract void Attack();

    protected virtual void Die() 
        => Destroy(gameObject);

    public abstract void Move();
}
