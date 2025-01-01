using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;
    public ObjectPool<Enemy> meleeCreepPool {get; private set;}
    Spawner spawner;
    
    void Awake()
    {
        spawner = GetComponent<Spawner>();
        meleeCreepPool = new ObjectPool<Enemy>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 50, 100);
    }

    Enemy CreatePooledItem ()
    {
        var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        return enemy;
    }

    void OnTakeFromPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    void OnReturnedToPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    void OnDestroyPoolObject (Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
