using System.Collections.Generic;
using UnityEngine;
public class TeslaTower : AbstractBaseTower
{
    //Electric Tower
    [SerializeField] private ParticleSystem electricEffectPrefab;

    protected override void InitializeAttackStrategy()
    {
        ITeslaEffectStrategy effectStrategy = new TeslaEffectStrategy(electricEffectPrefab);
        attackStrategy = new TeslaAttackStrategy(effectStrategy, GetTowerPosition());
    }
    protected override void InitializeTargetSelectionStrategy()
    {
        targetSelectionStrategy = new AllTargets();
    }
    public override List<ITargetSelectionStrategy> GetAvailableStrategies()
    {
        List<ITargetSelectionStrategy> strategies = new List<ITargetSelectionStrategy>();
        strategies.Add(new AllTargets());
        return strategies;
    }
}