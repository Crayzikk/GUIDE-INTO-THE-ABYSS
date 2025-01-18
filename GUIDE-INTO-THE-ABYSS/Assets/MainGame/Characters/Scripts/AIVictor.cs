using UnityEngine;

public class AIVictor : AICharacter
{
    protected override bool HasDetectedEnemy()
    {
        return base.HasDetectedEnemy();
        // Collider[] collidersEnemy = Physics.OverlapSphere(transform.position, radiusHasDetectEnemy, layerMaskEnemy);
        
        // foreach (var item in collidersEnemy)
        // {
        //     Debug.Log(item.name);
        //     if(layerMaskEnemy == LayerMask.NameToLayer("rustle") || layerMaskEnemy == LayerMask.NameToLayer("Enemy"))
        //     {
        //         return true;
        //     }
        // }

        // return false;
    }
}
