using System.Collections;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField] ProjectilePool projectilePool;
    [SerializeField] Transform projectileStartPoint;

    protected override void Update()
    {
        base.Update();

        if (currentHealthPoints < 1 && enabled)
        {
            enemyPool.rangeCreepPool.Release(this);
        }
    }

    protected override IEnumerator Attack(float attackFrequency)
    {
        while(isAttacking)
        {
            var projectile = projectilePool.rangeCreepProjectilePool.Get();
            projectile.transform.position = projectileStartPoint.position;
            projectile.transform.rotation = projectileStartPoint.rotation;
            yield return new WaitForSeconds(attackFrequency);
        };
    }

}