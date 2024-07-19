using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    [SerializeField] private EnemyHealthController _enemyHealthController;

    [SerializeField] private EnemyTypeSO _enemyTypeSO;

    private void Awake()
    {
        _enemyHealthController.OnHealthChanged.AddListener(ChangeSliderValue);
    }

    void Start()
    {
        SetSliderMinMaxValue();

        ChangeSliderValue(_enemyHealthController.CurrentHealth);
    }

    public void ChangeSliderValue(float value)
    {
        Debug.Log($"Slider value changed to: {value}");
        _slider.value = value;
    }

    private void SetSliderMinMaxValue()
    {
        _slider.minValue = 0;
        _slider.maxValue = _enemyTypeSO.MaxHealth;
    }

    private void OnDisable()
    {
        _enemyHealthController.OnHealthChanged.RemoveListener(ChangeSliderValue);
    }
}