using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 100;
    private IBaseHealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = new HealthSystem(_maxHealth);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        _healthSystem.TakeDamage(damage);

        UpdateUI();

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

    void UpdateUI()
    {
        InGameUIManager.Instance.UpdateBaseHealth(_maxHealth, _healthSystem.currentHealth);
    }
}