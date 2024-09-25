using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetDetector
{
    void InitializeTargetDetector(float detectionRadius);
    List<IAttackable> GetTargetsInRange();
    void AddTarget(IAttackable target);
    void RemoveTarget(IAttackable target);
    void UpdateRange(float newRange);
}