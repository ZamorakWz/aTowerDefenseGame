using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStrategy
{
    void Attack(IAttackable target, float damage);
    //void Attack(IEnumerable<IAttackable> targets, Vector3 towerPosition, float damage);
    IBulletMovementStrategy GetBulletMovementStrategy();
    BulletObjectPool.BulletType GetBulletType();
}