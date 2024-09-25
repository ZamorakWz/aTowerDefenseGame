using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenTower : AbstractBaseTower
{
    //Direct Attacker Tower

    protected override void InitializeAttackStrategy()
    {
        attackStrategy = new DirectAttackStrategy();
    }

    protected override void InitializeTargetSelectionStrategy()
    {
        targetSelectionStrategy = new NearestTarget();
    }
}