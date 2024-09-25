using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostHealthTarget : ITargetSelectionStrategy
{
    public IEnumerable<IAttackable> SelectTargets(List<IAttackable> targets, Vector3 towerPosition)
    {
        IAttackable mostHealthTarget = null;
        float maxHealth = float.MinValue;

        foreach (var target in targets)
        {
            float health = ((IHealthProvider)target).GetHealth();
            if (health > maxHealth)
            {
                maxHealth = health;
                mostHealthTarget = target;
            }
        }

        if (mostHealthTarget != null)
        {
            yield return mostHealthTarget;
        }
    }
}