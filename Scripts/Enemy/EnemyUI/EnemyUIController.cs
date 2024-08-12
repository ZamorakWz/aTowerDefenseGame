using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private EnemyTypeSO _enemyTypeSO;

    private void Awake()
    {
        EnemyHealthController.OnHealthChanged += HandleOnEnemyHealthChanged;
    }

    void Start()
    {
        SetSliderMinMaxValue();
    }

    private void OnDisable()
    {
        EnemyHealthController.OnHealthChanged -= HandleOnEnemyHealthChanged;
    }

    public void HandleOnEnemyHealthChanged(float value)
    {
        Debug.Log($"Slider value changed to: {value}");
        _slider.value = value;
    }

    private void SetSliderMinMaxValue()
    {
        _slider.minValue = 0;
        _slider.maxValue = _enemyTypeSO.MaxHealth;
    }
}