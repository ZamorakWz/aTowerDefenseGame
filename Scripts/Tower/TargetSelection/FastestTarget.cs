using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastestTarget : ITargetSelectionStrategy
{
    public IEnumerable<IAttackable> SelectTargets(List<IAttackable> targets, Vector3 towerPosition)
    {
        IAttackable fastestTarget = null;
        float maxSpeed = float.MinValue;

        foreach (var target in targets)
        {
            float speed = ((ISpeedProvider)target).GetSpeedValue();

            if (speed > maxSpeed)
            {
                maxSpeed = speed;
                fastestTarget = target;
            }
        }

        if (fastestTarget != null)
        {
            yield return fastestTarget;
        }
    }
}