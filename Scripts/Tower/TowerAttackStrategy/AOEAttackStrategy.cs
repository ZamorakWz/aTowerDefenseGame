using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEAttackStrategy : IAttackStrategy
{
    private float aoeRadius;

    public AOEAttackStrategy(float radius)
    {
        this.aoeRadius = radius;
    }

    public void Attack(IAttackable target, float damage)
    {
        Collider[] hitColliders = Physics.OverlapSphere(target.GetPosition(), aoeRadius);

        foreach (var hitCollider in hitColliders)
        {
            IAttackable attackable = hitCollider.GetComponent<IAttackable>();
            if (attackable != null)
            {
                attackable.TakeDamage(damage);
            }
        }
    }

    public IBulletMovementStrategy GetBulletMovementStrategy()
    {
        return new ArcBulletMoveStrategy();
    }

    public BulletObjectPool.BulletType GetBulletType()
    {
        return BulletObjectPool.BulletType.AOEBullet;
    }
}