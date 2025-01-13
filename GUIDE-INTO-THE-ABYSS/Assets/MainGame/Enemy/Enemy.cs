using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Enemy state
    [SerializeField] protected int health; 
    [SerializeField] protected float speed; 
    [SerializeField] protected int damage; 

    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public abstract void Attack();

    protected virtual void Die() 
        => Destroy(gameObject);

    public abstract void Move();
}
