using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestTargetStrategy : ITargetSelectionStrategy
{
    public IAttackable SelectTarget(List<IAttackable> targets, Vector3 towerPosition)
    {
        IAttackable nearestTarget = null;
        float nearestDistance = float.MaxValue;

        foreach (var target in targets)
        {
            float distance = Vector3.Distance(towerPosition, target.GetPosition());
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTarget = target;
            }
        }

        return nearestTarget;
    }
}