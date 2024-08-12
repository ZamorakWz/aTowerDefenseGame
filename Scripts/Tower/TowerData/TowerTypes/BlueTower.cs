using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTower : AbstractBaseTower
{
    //AOE Attacker Tower

    protected override void InitializeAttackStrategy()
    {
        attackStrategy = new AOEAttackStrategy(towerData.towerAOERadius);
    }

    protected override void InitializeTargetSelectionStrategy()
    {
        targetSelectionStrategy = new NearestTargetStrategy();
    }
}