using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] Countdown countdown;
    Coroutine coroutineSpawnMelee;
    Coroutine coroutineSpawnRange;
    ObjectPool<MeleeEnemy> meleeEnemyPool;
    ObjectPool<RangeEnemy> rangeEnemyPool;
    float frequencyOfMeleeCreepsSpawning = 0.5f;
    float frequencyOfMeleeRangeSpawning = 1f;


    void Start()
    {
        meleeEnemyPool = EnemyPool.Instance.meleeCreepPool;
        rangeEnemyPool = EnemyPool.Instance.rangeCreepPool;
        coroutineSpawnMelee = StartCoroutine(StartSpawningMelee());
        coroutineSpawnRange = StartCoroutine(StartSpawningRange());
    }

    IEnumerator StartSpawningMelee()
    {
        while (countdown.currentRoundTime > 0)
        {
            var randomSpawnSpot = Random.Range(0, spawnPoints.Count);
            var meleeEnemy = meleeEnemyPool.Get();
            meleeEnemy.gameObject.transform.SetPositionAndRotation(spawnPoints[randomSpawnSpot].transform.position, spawnPoints[randomSpawnSpot].transform.rotation);
            yield return new WaitForSeconds(frequencyOfMeleeCreepsSpawning);
        }
        StopCoroutine(coroutineSpawnMelee);
    }

    IEnumerator StartSpawningRange()
    {
        while (countdown.currentRoundTime > 0)
        {
            var randomSpawnSpot = Random.Range(0, spawnPoints.Count);
            var rangeEnemy = rangeEnemyPool.Get();
            rangeEnemy.transform.position = spawnPoints[randomSpawnSpot].transform.position;
            rangeEnemy.transform.rotation = spawnPoints[randomSpawnSpot].transform.rotation;
            yield return new WaitForSeconds(frequencyOfMeleeRangeSpawning);
        }
        StopCoroutine(coroutineSpawnRange);
    }
}
