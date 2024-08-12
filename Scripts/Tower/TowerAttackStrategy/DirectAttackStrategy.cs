using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectAttackStrategy : IAttackStrategy
{
    public void Attack(IAttackable target, float damage)
    {
        target.TakeDamage(damage);
    }

    public IBulletMovementStrategy GetBulletMovementStrategy()
    {
        return new DirectBulletMoveStrategy();
    }

    public BulletObjectPool.BulletType GetBulletType()
    {
        return BulletObjectPool.BulletType.StandartBullet;
    }
}