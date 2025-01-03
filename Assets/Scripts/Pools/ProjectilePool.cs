using System;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] RangeCreepProjectile rangeCreepProjectilePrefab;
    public ObjectPool<RangeCreepProjectile> rangeCreepProjectilePool {get; private set;}

    void Awake()
    {
        rangeCreepProjectilePool = new ObjectPool<RangeCreepProjectile>(CreateEnemyRangeProjectile, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject, true, 100, 200);
    }

    private RangeCreepProjectile CreateEnemyRangeProjectile()
    {
        var enemyRangeProjectile = Instantiate(rangeCreepProjectilePrefab, transform.position, Quaternion.identity);
        return enemyRangeProjectile;
    }

    private void OnTakeFromPool<T>(T projectile) where T : Component
    {
        projectile.gameObject.SetActive(true);
    }

    private void OnReturnToPool<T>(T projectile) where T : Component
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject<T>(T projectile) where T : Component
    {
        Destroy(projectile.gameObject);
    }
}
