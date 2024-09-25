using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTower : AbstractBaseTower
{
    //AOE Attacker Tower

    [SerializeField] private ParticleSystem aoeEffectPrefab;

    protected override void InitializeAttackStrategy()
    {
        IAOEEffectStrategy effectStrategy = new AOEEffectStrategy(aoeEffectPrefab);
        attackStrategy = new AOEAttackStrategy(towerData.towerAOERadius, effectStrategy);
    }

    protected override void InitializeTargetSelectionStrategy()
    {
        targetSelectionStrategy = new NearestTarget();
    }
}