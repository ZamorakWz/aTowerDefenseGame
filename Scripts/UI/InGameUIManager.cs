using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUIManager : MonoBehaviour, IObserverUI
{
    public static InGameUIManager Instance {  get; private set; }

    [SerializeField] private TextMeshProUGUI _baseHealth;
    [SerializeField] private TextMeshProUGUI _currentWave;
    [SerializeField] private TextMeshProUGUI _aliveEnemyCount;
    [SerializeField] private TextMeshProUGUI _timeToNextWave;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateBaseHealth(int maxHealth, int currentHealth)
    {
        _baseHealth.text = $"Base Health {maxHealth}/{currentHealth}";
    }

    public void UpdateCurrentWave(int maxWave, int currentWave)
    {
        _currentWave.text = $"{currentWave}/{maxWave}";
    }

    public void UpdateAliveEnemyCount(int count)
    {
        _aliveEnemyCount.text = $"Alive Enemy Count: {count}";
    }

    public void UpdateTimeToNextWave(int time)
    {
        _timeToNextWave.text = $"Time To Next Wave: {time}";
    }
}