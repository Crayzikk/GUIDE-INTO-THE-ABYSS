using UnityEngine;

public class Beholder : Enemy
{
    protected override int health { get; set; } = 120;
    protected override int damage { get; set; } = 25;
    protected override float attackRate { get; set; } = 3f;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private float nextTimeToShoot = 0f;

    void Update()
    {
        enemyAttackTriger.shootStartAttack = true;
        base.Update();
    
        if(enemyAttack && Time.time >= nextTimeToShoot && currentTrigger != null)
        {
            nextTimeToShoot = attackRate + Time.time;
            ShootProjectile();
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direction = (currentTrigger.position - firePoint.position).normalized;
                rb.AddForce(direction * 10f, ForceMode.VelocityChange);
            }
        }
    }
}
