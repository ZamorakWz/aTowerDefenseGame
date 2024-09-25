using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetSelectionStrategy
{
    //IAttackable SelectTarget(List<IAttackable> targets, Vector3 towerPosition);

    IEnumerable<IAttackable> SelectTargets(List<IAttackable> targets, Vector3 towerPosition);
}