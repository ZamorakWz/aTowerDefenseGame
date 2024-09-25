using UnityEngine;
public class TeslaAttackStrategy : IAttackStrategy
{
    private ITeslaEffectStrategy effectStrategy;
    private Vector3 towerPosition;

    public TeslaAttackStrategy(ITeslaEffectStrategy effectStrategy, Vector3 towerPosition)
    {
        this.effectStrategy = effectStrategy;
        this.towerPosition = towerPosition;
    }

    public void Attack(IAttackable target, float damage)
    {
        Vector3 targetPosition = ((IPositionProvider)target).GetPosition();

        if (towerPosition == Vector3.zero)
        {
            Debug.LogError("Tower position has not been initialized correctly.");
            return;
        }

        target.TakeDamage(damage);

        float distance = Vector3.Distance(towerPosition, targetPosition);

        effectStrategy.CreateTeslaEffect(towerPosition, targetPosition, distance);
    }

    public IBulletMovementStrategy GetBulletMovementStrategy()
    {
        return new TeslaBulletMoveStrategy();
    }

    public BulletObjectPool.BulletType GetBulletType()
    {
        return BulletObjectPool.BulletType.ElectricBullet;
    }
}