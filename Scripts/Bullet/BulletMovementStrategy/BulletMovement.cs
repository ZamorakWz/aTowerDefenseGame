using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 10f;
    private float _damage;
    private IAttackable target;
    private IAttackStrategy attackStrategy;

    public void SetBulletTarget(IAttackable target, float damage, IAttackStrategy attackStrategy)
    {
        this.target = target;
        this._damage = damage;
        this.attackStrategy = attackStrategy;

        Vector3 targetPostion = target.GetPosition();
        var movementStrategy = attackStrategy.GetBulletMovementStrategy();
        movementStrategy.MoveBullet(transform, targetPostion, _bulletSpeed, OnHitTarget);
    }

    private void OnHitTarget()
    {
        attackStrategy.Attack(target, _damage);

        gameObject.SetActive(false);
    }
}