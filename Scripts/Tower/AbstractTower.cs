using UnityEngine;

public abstract class AbstractTower : MonoBehaviour
{
    [SerializeField] private TowerTypeSO _towerData;

    protected float _lastFireTime;

    public virtual void Initialize(TowerTypeSO towerData)
    {
        _towerData = towerData;
    }

    public abstract void Shoot();

    public virtual void Upgrade()
    {
        _towerData.towerTypeDamage *= 1.1f;
        _towerData.towerTypeFireRate *= 1.1f;
    }

    protected virtual void Update()
    {
        if (Time.time - _lastFireTime >= 1f / _towerData.towerTypeFireRate)
        {
            Shoot();
            _lastFireTime = Time.time;
        }
    }
}