using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthController : MonoBehaviour
{
    [System.NonSerialized]
    public UnityEvent<float> OnHealthChanged;

    private EnemySpawnController _enemySpawnController;

    [SerializeField] private EnemyTypeSO _enemyTypeSO;

    [SerializeField] private float _currentHealth;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    private void Awake()
    {
        SetEnemyHealthBeginningValue();
        _enemySpawnController = FindObjectOfType<EnemySpawnController>();
    }

    private void OnEnable()
    {
        if (OnHealthChanged == null)
        {
            OnHealthChanged = new UnityEvent<float>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(28f);
        }
    }

    public void SetEnemyHealthBeginningValue()
    {
        _currentHealth = _enemyTypeSO.MaxHealth;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            Die();
        }

        Debug.Log("Damage is taken!");

        OnHealthChanged.Invoke(_currentHealth);
    }

    public void Die()
    {
        gameObject.SetActive(false);

        _enemySpawnController.AliveEnemyCount--;
    }
}