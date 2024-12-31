using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] Transform playerPosition;
    [SerializeField] private float distanceToAttack = 2f;
    NavMeshAgent agent;
    float rotationSpeedSlowdownMultiplier = 2f;
    float rotationSpeed = 3f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        agent.SetDestination(playerPosition.position);

        if (Vector3.Distance(transform.position, playerPosition.position) <= distanceToAttack)
        {
            Vector3 direction = playerPosition.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}