using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBaseTower : MonoBehaviour
{
    [SerializeField] protected TowerTypeSO towerData;
    [SerializeField] protected Transform firePoint;
    protected float lastAttackTime;
    protected IAttackStrategy attackStrategy;
    protected ITargetDetector targetDetector;
    protected ITargetSelectionStrategy targetSelectionStrategy;
    protected IAttackable currentTarget;
    protected IAttackManager attackManager;
    protected bool isTowerPlaced;

    protected virtual void OnEnable()
    {
        SubscribeEvent();
        AttackRoutine().Forget();
    }

    protected virtual void OnDestroy()
    {
        UnSubscribeEvent();
    }

    protected async UniTaskVoid AttackRoutine()
    {
        while (isActiveAndEnabled)
        {
            if (isTowerPlaced)
            {
                PerformAttackTasks();
            }
            await UniTask.Delay(100);
        }
    }

    protected void PerformAttackTasks()
    {
        if (currentTarget == null || !IsTargetValid(currentTarget))
        {
            currentTarget = SelectNewTarget();
        }

        if (currentTarget != null)
        {
            attackManager.Attack(currentTarget);
        }
    }

    public virtual void Initialize(TowerTypeSO towerData)
    {
        this.towerData = towerData;
        targetDetector = gameObject.GetComponent<SphereTargetDetector>();
        targetDetector.InitializeTargetDetector(towerData.towerRange);

        InitializeAttackStrategy();

        InitializeTargetSelectionStrategy();

        attackManager = new AttackManager(attackStrategy,
             towerData.towerFireRate,
             towerData.towerDamage, firePoint);
    }

    protected abstract void InitializeAttackStrategy();

    protected abstract void InitializeTargetSelectionStrategy();

    public TowerTypeSO GetTowerData()
    {
        return towerData;
    }

    public List<IAttackable> GetTargetsInRange()
    {
        return targetDetector.GetTargetsInRange();
    }

    protected IAttackable SelectNewTarget()
    {
        return targetSelectionStrategy.SelectTarget(GetTargetsInRange(), transform.position);
    }

    protected bool IsTargetValid(IAttackable target)
    {
        return GetTargetsInRange().Contains(target);
    }

    protected void OnTowerPlaced(GameObject tower)
    {
        if (tower == this.gameObject)
        {
            isTowerPlaced = true;
        }
    }

    protected void SubscribeEvent()
    {
        TowerPlacementManager.OnTowerPlaced += OnTowerPlaced;
    }

    protected void UnSubscribeEvent()
    {
        TowerPlacementManager.OnTowerPlaced -= OnTowerPlaced;
    }
}