using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseHealthSystem
{
    int currentHealth { get; }
    int maxHealth { get; }
    bool isDead { get; }
    void TakeDamage (int damage);
    void Heal(int amount);
}