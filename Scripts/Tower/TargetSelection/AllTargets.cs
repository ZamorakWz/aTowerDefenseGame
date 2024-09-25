using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTargets : ITargetSelectionStrategy
{
    public IEnumerable<IAttackable> SelectTargets(List<IAttackable> targets, Vector3 towerPosition)
    {
        return targets;
    }
}