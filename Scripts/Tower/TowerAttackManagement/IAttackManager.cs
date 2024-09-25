using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackManager
{
    void Attack(IEnumerable<IAttackable> targets);
    bool CanAttack();
    void UpdateDamage(float newDamage);
    void UpdateFireRate(float newFireRate);
}