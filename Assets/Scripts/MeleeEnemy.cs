using System.Collections;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    protected override void Update()
    {
        base.Update();
        
        if(currentHealthPoints < 1 && enabled)
        {
            enemyPool.meleeCreepPool.Release(this);
        }
    }

        protected override IEnumerator Attack(float attackFrequency)
    {
        while(isAttacking)
        {
            Debug.Log($"Player is attacked by {gameObject.name}");
            yield return new WaitForSeconds(attackFrequency);
        };
    }
}
