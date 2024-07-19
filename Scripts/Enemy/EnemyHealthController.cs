using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private EnemyTypeSO _enemyTypeSO;

    [SerializeField] private float _currentHealth;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public UnityEvent<float> OnHealthChanged;

    private void OnEnable()
    {
        if (OnHealthChanged == null)
        {
            OnHealthChanged = new UnityEvent<float>();
        }
    }

    private void Awake()
    {
        SetEnemyHealthBeginingValue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(18f);
        }
    }

    public void SetEnemyHealthBeginingValue()
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

        Debug.Log("Damage taken!");

        OnHealthChanged.Invoke(_currentHealth);
    }

    private void Die()
    {
        gameObject.SetActive(false);

        //add back to the queue
    }
}
