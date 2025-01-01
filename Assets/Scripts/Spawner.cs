using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] int amountOfCreeps = 50;
    EnemyPool enemyPool;
    ObjectPool<Enemy> meleeEnemyPool;
    float frequencyOfCreepsSpawn = 0.5f;
    

    void Start()
    {
        enemyPool = GetComponent<EnemyPool>();
        meleeEnemyPool = enemyPool.meleeCreepPool;
        StartCoroutine(StartSpawningCreeps(amountOfCreeps));
    }

    IEnumerator StartSpawningCreeps(int amount)
    {
        while(amount > 1)
        {
            var randomSpawnSpot = Random.Range(0, spawnPoints.Count);
            var meleeEnemy = meleeEnemyPool.Get();
            meleeEnemy.transform.position = spawnPoints[randomSpawnSpot].transform.position;
            meleeEnemy.transform.rotation = spawnPoints[randomSpawnSpot].transform.rotation;
            amount--;
            yield return new WaitForSeconds(frequencyOfCreepsSpawn);
        }
        StopCoroutine(StartSpawningCreeps(amountOfCreeps));
    }
}
