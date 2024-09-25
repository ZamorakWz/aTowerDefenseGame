using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostProgressTarget : ITargetSelectionStrategy
{
    public IEnumerable<IAttackable> SelectTargets(List<IAttackable> targets, Vector3 towerPosition)
    {
        IAttackable mostProgressTarget = null;
        float shortestDistance = float.MaxValue;

        Vector3 basePosition = BasePosition.Instance.GetBasePosition();

        for (int i = 0; i < targets.Count; i++)
        {
            var target = targets[i];
            if (target is IPositionProvider positionProvider)
            {
                float distanceToBase = Vector3.Distance(positionProvider.GetPosition(), basePosition);

                if (distanceToBase < shortestDistance)
                {
                    shortestDistance = distanceToBase;
                    mostProgressTarget = target;
                }
            }
        }

        if (mostProgressTarget != null)
        {
            yield return mostProgressTarget;
        }
    }
}