using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : IAttackManager
{
    private IAttackStrategy _attackStrategy;
    private float _fireRate;
    private float _damage;
    private float _lastAttackTime;
    private Transform _firePoint;

    public AttackManager(IAttackStrategy attackStrategy, float fireRate, float damage, Transform firePoint)
    {
        _attackStrategy = attackStrategy;
        _fireRate = fireRate;
        _damage = damage;
        _firePoint = firePoint;
    }

    public void UpdateDamage(float newDamage)
    {
        _damage = newDamage;
    }
    
    public void UpdateFireRate(float newFireRate)
    {
        _fireRate = newFireRate;
    }

    public void Attack(IEnumerable<IAttackable> targets)
    {
        if (CanAttack())
        {
            foreach (var target in targets)
            {
                FireBullet(target);
            }
            _lastAttackTime = Time.time;
        }
    }

    private void FireBullet(IAttackable target)
    {
        GameObject bullet = BulletObjectPool.Instance.GetPooledBullet(_attackStrategy.GetBulletType());
        bullet.transform.position = _firePoint.position;

        BulletMovement bulletMovement = bullet.GetComponent<BulletMovement>();
        bulletMovement.SetBulletTarget(target, _damage, _attackStrategy);
    }

    public bool CanAttack()
    {
        return Time.time - _lastAttackTime >= 1f / _fireRate;
    }
}