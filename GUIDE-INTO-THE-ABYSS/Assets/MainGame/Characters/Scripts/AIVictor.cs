using UnityEngine;

public class AIVictor : AICharacter
{
    protected override bool HasDetectedEnemy()
    {
        Collider[] collidersEnemy = Physics.OverlapSphere(transform.position, radiusHasDetectEnemy, 
                                LayerMask.GetMask("Enemy") | LayerMask.GetMask("rustle"));

        return SetDetectedEnemy(collidersEnemy);
    }
}
