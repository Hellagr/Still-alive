using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance { get; private set; }

    [SerializeField] MeleeEnemy meleeEnemyPrefab;
    [SerializeField] RangeEnemy rangeEnemyPrefab;
    public ObjectPool<MeleeEnemy> meleeCreepPool { get; private set; }
    public ObjectPool<RangeEnemy> rangeCreepPool { get; private set; }

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

        if (meleeEnemyPrefab == null)
        {
            Debug.LogError($"{meleeEnemyPrefab} is not assigned!");
        }
        if (rangeEnemyPrefab == null)
        {
            Debug.LogError($"{rangeEnemyPrefab} is not assigned!");
        }

        meleeCreepPool = new ObjectPool<MeleeEnemy>(CreatePooledItemMelee, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 50, 100);
        rangeCreepPool = new ObjectPool<RangeEnemy>(CreatePooledItemRange, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 50, 100);

        //for (int i = 0; i < 50; i++)
        //{
        //    meleeCreepPool.Get();
        //    rangeCreepPool.Get();
        //}
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
