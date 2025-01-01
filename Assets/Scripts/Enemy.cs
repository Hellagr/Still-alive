using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] EnemyPool enemyPool;
    [SerializeField] Transform playerPosition;
    [SerializeField] private float distanceToAttack = 2f;
    [SerializeField] private int healthPoint = 1;
    NavMeshAgent agent;
    float rotationSpeed = 3f;

    public int HealthPoint
    {
        get 
        { 
            return healthPoint; 
        }
        set 
        { 
            healthPoint = value; 
        }
    }

    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected void Update()
    {
        agent.SetDestination(playerPosition.position);

        if (Vector3.Distance(transform.position, playerPosition.position) <= distanceToAttack)
        {
            Vector3 direction = playerPosition.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if(healthPoint < 1)
        {
            enemyPool.meleeCreepPool.Release(this);
        }
    }
}