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

        if (target is IPositionProvider positionProvider)
        {
            Vector3 targetPosition = positionProvider.GetPosition();
            var movementStrategy = attackStrategy.GetBulletMovementStrategy();
            movementStrategy.MoveBullet(transform, targetPosition, _bulletSpeed, OnHitTarget);
        }
    }

    private void OnHitTarget()
    {
        if (target is IAttackable attackable)
        {
            attackStrategy.Attack(attackable, _damage);
        }

        gameObject.SetActive(false);
    }
}