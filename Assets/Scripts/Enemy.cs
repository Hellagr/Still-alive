using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public abstract class Enemy : MonoBehaviour
{
    //[SerializeField] protected EnemyPool enemyPool;
    [SerializeField] private float distanceToAttack = 2f;
    [SerializeField] private int healthPoint = 1;
    [SerializeField] float attackFrequency = 1f;
    //Transform playerPosition;
    NavMeshAgent agent;
    Coroutine attackCoroutine;

    //protected ObjectPool<ParticleSystem> particleExpiriencePool;
    protected bool isAttacking = false;
    public int currentHealthPoints { get; private set; }
    float rotationSpeed = 3f;

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

        //if (playerPosition == null)
        //{
        //    playerPosition = GameManager.Instance.PlayerPosition;
        //}

        //if (particleExpiriencePool == null)
        //{
        //    particleExpiriencePool = ParticlePool.Instance.particleExpiriencePool;
        //}
    }

    protected virtual void Update()
    {
        agent.SetDestination(GameManager.Instance.PlayerPosition.position);

        var currentDistanceToPlayer = Vector3.Distance(transform.position, GameManager.Instance.PlayerPosition.position);

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
        Vector3 direction = GameManager.Instance.PlayerPosition.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    protected abstract IEnumerator Attack(float attackFrequency);
    protected abstract void Death();
}