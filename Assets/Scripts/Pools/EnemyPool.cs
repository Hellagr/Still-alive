using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] MeleeEnemy meleeEnemyPrefab;
    [SerializeField] RangeEnemy rangeEnemyPrefab;
    public ObjectPool<MeleeEnemy> meleeCreepPool { get; private set; }
    public ObjectPool<RangeEnemy> rangeCreepPool { get; private set; }
    Spawner spawner;

    void Awake()
    {
        if (meleeEnemyPrefab == null)
        {
            Debug.LogError($"{meleeEnemyPrefab} is not assigned!");
        }
        if (rangeEnemyPrefab == null)
        {
            Debug.LogError($"{rangeEnemyPrefab} is not assigned!");
        }

        spawner = GetComponent<Spawner>();
        meleeCreepPool = new ObjectPool<MeleeEnemy>(CreatePooledItemMelee, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 50, 100);
        rangeCreepPool = new ObjectPool<RangeEnemy>(CreatePooledItemRange, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 50, 100);
    }

    MeleeEnemy CreatePooledItemMelee() 
    {
        var enemy = Instantiate(meleeEnemyPrefab, transform.position, Quaternion.identity);
        return enemy;
    }

    RangeEnemy CreatePooledItemRange() 
    {
        var enemy = Instantiate(rangeEnemyPrefab, transform.position, Quaternion.identity);
        return enemy;
    }

    void OnTakeFromPool<T>(T enemy) where T : Component
    {
        enemy.gameObject.SetActive(true);
    }

    void OnReturnedToPool<T>(T enemy) where T : Component
    {
        enemy.gameObject.SetActive(false);
    }

    void OnDestroyPoolObject<T>(T enemy) where T : Component
    {
        Destroy(enemy.gameObject);
    }
}
