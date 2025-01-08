using System;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }
    [SerializeField] ProjectileRangeCreep rangeCreepProjectilePrefab;
    public ObjectPool<ProjectileRangeCreep> rangeCreepProjectilePool { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rangeCreepProjectilePool = new ObjectPool<ProjectileRangeCreep>(CreateEnemyRangeProjectile, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject, true, 100, 200);
    }

    private ProjectileRangeCreep CreateEnemyRangeProjectile()
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
