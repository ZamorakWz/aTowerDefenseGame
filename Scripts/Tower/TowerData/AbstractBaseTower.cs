using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class AbstractBaseTower : MonoBehaviour
{
    [SerializeField] protected TowerTypeSO towerData;
    [SerializeField] protected Transform firePoint;

    //Attributes for each tower
    public float towerDamage { get; private set; }
    public float towerFireRate { get; private set; }
    public float towerRange { get; private set; }

    //Upgrade levels for each property
    public int damageUpgradeLevel { get; private set; } = 0;
    public int rangeUpgradeLevel { get; private set; } = 0;
    public int fireRateUpgradeLevel { get; private set; } = 0;

    private int baseUpgradeCost = 4;

    protected float lastAttackTime;

    protected IAttackStrategy attackStrategy;
    protected ITargetDetector targetDetector;
    public ITargetSelectionStrategy targetSelectionStrategy;
    protected IAttackable currentTarget;
    protected IAttackManager attackManager;
    protected ITowerRangeUpdater towerRangeVisualizer;

    public bool isTowerPlaced { get; private set; }

    protected Vector3 towerPosition;

    private TowerDataUI towerDataUI;

    #region ------------------------------MONOBEHAVIOURS------------------------------
    private void Awake()
    {
        towerDataUI = GetComponent<TowerDataUI>();
    }

    protected virtual void OnEnable()
    {
        SubscribeEvent();
    }

    protected virtual void OnDestroy()
    {
        UnSubscribeEvent();
    }
    #endregion

    #region ------------------------------UPGRADE------------------------------
    public float GetUpgradeCost(int level)
    {
        return baseUpgradeCost * Mathf.Pow(2, level);
    }

    public void UpgradeDamage()
    {
        int cost = (int)GetUpgradeCost(damageUpgradeLevel);
        if (GoldManager.Instance.GetCurrentGold() >= cost)
        {
            GoldManager.Instance.RemoveGold(cost);
            damageUpgradeLevel++;
            towerDamage *= 1.1f;

            //update tower damage
            if (attackManager != null)
            {
                attackManager.UpdateDamage(towerDamage);
            }
        }
    }

    public void UpgradeRange()
    {
        int cost = (int)GetUpgradeCost(rangeUpgradeLevel);
        if (GoldManager.Instance.GetCurrentGold() >= cost)
        {
            if (towerRangeVisualizer == null)
            {
                Debug.LogError("towerRangeVisualizer (ITowerRangeUpdater) is null!");
                return;
            }

            GoldManager.Instance.RemoveGold(cost);
            rangeUpgradeLevel++;
            towerRange *= 1.1f;

            //update radius value from targetdetector and visual
            targetDetector.UpdateRange(towerRange);
            towerRangeVisualizer.UpdateTowerRangeVisualization(towerRange);
        }
    }

    public void UpgradeFireRate()
    {
        int cost = (int)GetUpgradeCost(fireRateUpgradeLevel);
        if (GoldManager.Instance.GetCurrentGold() >= cost)
        {
            GoldManager.Instance.RemoveGold(cost);
            fireRateUpgradeLevel++;
            towerFireRate *= 1.1f;

            //update firerate value from attackmanager
            if (attackManager != null)
            {
                attackManager.UpdateFireRate(towerFireRate);
            }
        }
    }
    #endregion

    #region------------------------------ATTACK------------------------------
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

    protected virtual void PerformAttackTasks()
    {
        if (currentTarget == null || !IsTargetValid(currentTarget))
        {
            SelectNewTarget();
        }

        if (attackManager.CanAttack())
        {
            var targets = targetSelectionStrategy.SelectTargets(targetDetector.GetTargetsInRange(), transform.position);
            attackManager.Attack(targets);
        }
    }
    #endregion

    #region------------------------------TOWER INITIALIZE------------------------------
    public virtual void Initialize(TowerTypeSO towerData)
    {
        this.towerData = towerData;

        //Make independence the informations that come from scriptable objects
        towerDamage = towerData.towerDamage;
        towerFireRate = towerData.towerFireRate;
        towerRange = towerData.towerRange;

        //Setup targetdetection
        targetDetector = gameObject.GetComponent<SphereTargetDetector>();
        targetDetector.InitializeTargetDetector(towerRange);

        //TowerRangeVisualizer
        towerRangeVisualizer = gameObject.GetComponent<ITowerRangeUpdater>();

        //Setup strategies
        InitializeAttackStrategy();
        InitializeTargetSelectionStrategy();

        //Setup attackmanager
        attackManager = new AttackManager(attackStrategy,
             towerFireRate,
             towerDamage, firePoint);
    }

    protected abstract void InitializeAttackStrategy();

    protected abstract void InitializeTargetSelectionStrategy();

    public TowerTypeSO GetTowerData()
    {
        return towerData;
    }
    #endregion

    #region------------------------------TOWER PLACEMENT------------------------------
    protected Vector3 GetTowerPosition()
    {
        return towerPosition != Vector3.zero ? towerPosition : transform.position;
    }

    protected virtual void HandleTowerPlaced(GameObject tower)
    {
        if (tower == null)
        {
            Debug.LogError("The tower object has already been destroyed.");
            return;
        }

        if (tower != null && tower.activeInHierarchy && tower == this.gameObject)
        {
            isTowerPlaced = true;

            towerPosition = transform.position;

            Initialize(towerData);

            AttackRoutine().Forget();

            Debug.Log($"{tower.gameObject.name} has been fully initialized and attack routine has started.");
        }
    }
    #endregion

    #region------------------------------TARGET SELECTION------------------------------
    public List<IAttackable> GetTargetsInRange()
    {
        return targetDetector.GetTargetsInRange().OfType<IAttackable>().ToList();
    }

    protected virtual void SelectNewTarget()
    {
        var targets = targetDetector.GetTargetsInRange();
        var selectedTargets = targetSelectionStrategy.SelectTargets(targets, transform.position);
        currentTarget = selectedTargets.FirstOrDefault();
    }

    protected bool IsTargetValid(IAttackable target)
    {
        bool isValid = GetTargetsInRange().Contains(target);
        return isValid;
    }
    #endregion

    #region------------------------------TARGET SELECTION STRATEGY------------------------------
    public virtual List<ITargetSelectionStrategy> GetAvailableStrategies()
    {
        List<ITargetSelectionStrategy> strategies = new List<ITargetSelectionStrategy>();
        strategies.Add(new NearestTarget());
        strategies.Add(new FastestTarget());
        strategies.Add(new MostHealthTarget());
        strategies.Add(new MostProgressTarget());
        return strategies;
    }

    public void ChangeTargetSelectionStrategy(ITargetSelectionStrategy newStrategy)
    {
        List<ITargetSelectionStrategy> availableStrategies = GetAvailableStrategies();

        bool isStrategyAvailable = false;
        foreach (var strategy in availableStrategies)
        {
            if (strategy.GetType() == newStrategy.GetType())
            {
                isStrategyAvailable = true;
                break;
            }

            if (isStrategyAvailable)
            {
                targetSelectionStrategy = newStrategy;
            }
        }
    }
    #endregion

    #region------------------------------Events------------------------------
    protected void SubscribeEvent()
    {
        TowerPlacementManager.OnTowerPlaced += HandleTowerPlaced;
    }

    protected void UnSubscribeEvent()
    {
        TowerPlacementManager.OnTowerPlaced -= HandleTowerPlaced;
    }
    #endregion
}