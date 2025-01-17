using UnityEngine;

public class EnemyAttackTriger : MonoBehaviour
{
    public bool enemyStartAttack;
    public bool shootStartAttack;

    private int damageEnemy;

    void OnTriggerStay(Collider other)
    {
        if(enemyStartAttack && !shootStartAttack)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Character"))
            {
                if(other.CompareTag("Player"))
                {
                    other.GetComponent<Health>().TakeDamage(damageEnemy);
                }
            }

            enemyStartAttack = false;           
        }
    }

    public void SetDamageAttack(int damage)
    {
        damageEnemy = damage;
    }
}
