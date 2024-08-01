using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, IDamageable
{
    public static Action<int, int> OnBaseHealthChanged;

    [SerializeField] private int _maxHealth = 100;
    private IBaseHealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = new HealthSystem(_maxHealth);

        OnBaseHealthChanged?.Invoke(_maxHealth, _healthSystem.currentHealth);
    }

    public void TakeDamage(int damage)
    {
        _healthSystem.TakeDamage(damage);

        OnBaseHealthChanged?.Invoke(_maxHealth, _healthSystem.currentHealth);

        if (_healthSystem.isDead)
        {
            OnBaseDeath();
        }
    }

    private void OnBaseDeath()
    {
        Debug.Log("Base is destroyed! Game Over!");

        Time.timeScale = 0;
    }
}