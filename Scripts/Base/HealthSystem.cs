using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : IBaseHealthSystem
{
    public int currentHealth { get; private set; }

    public int maxHealth { get; }
    public bool isDead => currentHealth <= 0;

    public HealthSystem(int _maxHealth)
    {
        maxHealth = _maxHealth;
        currentHealth = _maxHealth;
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
    }
}
