using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EnemyUIController : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private EnemyTypeSO enemyTypeSO;

    private float maxHealth;

    private EnemyHealthController healthController;

    private void Awake()
    {
        healthController = GetComponent<EnemyHealthController>();
    }

    private void OnEnable()
    {
        healthController.OnHealthChanged += HandleOnEnemyHealthChanged;

        UpdateHealthUI(enemyTypeSO.typeMaxHealth);

        maxHealth = enemyTypeSO.typeMaxHealth;
    }

    private void OnDisable()
    {
        healthController.OnHealthChanged -= HandleOnEnemyHealthChanged;
    }

    public void HandleOnEnemyHealthChanged(float currentHealth)
    {
        UpdateHealthUI(currentHealth);
    }

    private void UpdateHealthUI(float currentHealth)
    {
        float healthPercentage = currentHealth / maxHealth;

        healthImage.fillAmount = Mathf.Clamp01(healthPercentage);

        Debug.Log($"Health changed: {currentHealth}/{maxHealth}, Fill amount: {healthImage.fillAmount}");
    }
}