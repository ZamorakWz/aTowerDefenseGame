using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackManager
{
    void Attack(IAttackable attackable);
    bool CanAttack();
}