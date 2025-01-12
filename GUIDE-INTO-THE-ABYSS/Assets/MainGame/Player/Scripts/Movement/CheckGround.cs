using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public bool playerTouchGround;

    private float sphereRadius = 0.2f;

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sphereRadius);

        foreach (Collider c in colliders)
        {
            if(c.CompareTag("Ground"))
            {
                playerTouchGround = true;
                break;
            }
        }
    }
}
