using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    //[SerializeField] protected EnemyPool enemyPool;
    [SerializeField] private float distanceToAttack = 2f;
    [SerializeField] private int healthPoint = 1;
    [SerializeField] float attackFrequency = 1f;
    Transform playerPosition;
    NavMeshAgent agent;
    Coroutine attackCoroutine;

    public int currentHealthPoints { get; private set; }
    float rotationSpeed = 3f;
    protected bool isAttacking = false;

    public int ReceiveDamage(int value)
    {
        return currentHealthPoints -= value;
    }

    void Awake()
    {

    }

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealthPoints = healthPoint;
        if (playerPosition == null)
        {
            playerPosition = GameManager.Instance.PlayerPosition;
        }
    }

    protected virtual void Update()
    {
        agent.SetDestination(playerPosition.position);

        var currentDistanceToPlayer = Vector3.Distance(transform.position, playerPosition.position);

        if (currentDistanceToPlayer <= distanceToAttack)
        {
            RotationOnSpot();

            if (!isAttacking)
            {
                isAttacking = true;

                if (attackCoroutine == null)
                {
                    attackCoroutine = StartCoroutine(Attack(attackFrequency));
                }
                else
                {
                    StartCoroutine(Attack(attackFrequency));
                }
            }
        }
        else if (currentDistanceToPlayer > distanceToAttack && isAttacking)
        {
            isAttacking = false;
            StopCoroutine(attackCoroutine);
        }
    }

    private void RotationOnSpot()
    {
        Vector3 direction = playerPosition.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    protected abstract IEnumerator Attack(float attackFrequency);
}