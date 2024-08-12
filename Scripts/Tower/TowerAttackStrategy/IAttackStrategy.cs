using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStrategy
{
    void Attack(IAttackable target, float damage);
    IBulletMovementStrategy GetBulletMovementStrategy();
    BulletObjectPool.BulletType GetBulletType();
}