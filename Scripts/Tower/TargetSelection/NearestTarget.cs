using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestTarget : ITargetSelectionStrategy
{
    public IEnumerable<IAttackable> SelectTargets(List<IAttackable> targets, Vector3 towerPosition)
    {
        IAttackable nearestTarget = null;
        float nearestDistance = float.MaxValue;

        foreach (var target in targets)
        {
            float distance = Vector3.Distance(towerPosition, ((IPositionProvider)target).GetPosition());

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTarget = target;
            }
        }

        if (nearestTarget != null)
        {
            yield return nearestTarget;
        }
    }
}