using System.Collections;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    int rewardForKilling = 1;
    protected override void Update()
    {
        base.Update();

        if (currentHealthPoints < 1 && enabled)
        {
            Die();
            EnemyPool.Instance.meleeCreepPool.Release(this);
        }
    }

    protected override IEnumerator Attack(float attackFrequency)
    {
        while (isAttacking)
        {
            Debug.Log($"Player is attacked by {gameObject.name}");
            yield return new WaitForSeconds(attackFrequency);
        };
    }

    protected override void Die()
    {
        var particle = ParticlePool.Instance.particleExperiencePool.Get();
        particle.SetAmountOfExpirience(rewardForKilling);
        particle.transform.position = transform.position;
    }
}
