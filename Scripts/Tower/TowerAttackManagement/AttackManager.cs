using System;
using UnityEngine;

public class AttackManager : IAttackManager
{
    private readonly IAttackStrategy _attackStrategy;
    private readonly float _fireRate;
    private readonly float _damage;
    private float _lastAttackTime;
    private Transform _firePoint;

    public AttackManager(IAttackStrategy attackStrategy, float fireRate, float damage, Transform firePoint)
    {
        _attackStrategy = attackStrategy;
        _fireRate = fireRate;
        _damage = damage;
        _firePoint = firePoint ?? throw new ArgumentNullException(nameof(firePoint));
    }

    public void Attack(IAttackable target)
    {
        if (CanAttack())
        {
            FireBullet(target);
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