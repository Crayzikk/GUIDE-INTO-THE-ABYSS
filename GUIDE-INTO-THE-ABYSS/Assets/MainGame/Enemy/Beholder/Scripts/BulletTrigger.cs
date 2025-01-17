using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    private int damage = 25;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            if(other.CompareTag("Player"))
            {
                other.GetComponent<Health>().TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
