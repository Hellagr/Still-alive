using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class RangeEnemy : Enemy
{
    [SerializeField] Transform projectileStartPoint;
    ObjectPool<ProjectileRangeCreep> projectilePool;
    Transform playerTransform;

    void Start()
    {
        if (projectilePool == null)
        {
            projectilePool = ProjectilePool.Instance.rangeCreepProjectilePool;
        }

        playerTransform = GameManager.Instance.PlayerPosition;
    }

    protected override void Update()
    {
        base.Update();

        if (currentHealthPoints < 1 && enabled)
        {
            EnemyPool.Instance.rangeCreepPool.Release(this);
        }

        projectileStartPoint.LookAt(playerTransform.position);
    }

    protected override IEnumerator Attack(float attackFrequency)
    {
        while (isAttacking)
        {
            var projectile = projectilePool.Get();
            projectile.transform.position = projectileStartPoint.position;
            projectile.transform.rotation = projectileStartPoint.rotation;
            yield return new WaitForSeconds(attackFrequency);
        };
    }

}