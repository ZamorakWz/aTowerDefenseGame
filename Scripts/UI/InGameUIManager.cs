using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUIManager : MonoBehaviour, IIngameUI
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

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    public void HandleBaseHealth(int maxHealth, int currentHealth)
    {
        _baseHealth.text = $"Base Health {maxHealth}/{currentHealth}";
    }

    public void HandleCurrentWave(int maxWave, int currentWave)
    {
        _currentWave.text = $"{currentWave}/{maxWave}";
    }

    public void HandleAliveEnemyCount(int count)
    {
        _aliveEnemyCount.text = $"Alive Enemy Count: {count}";
    }

    public void HandleRemainingTimeToNextWave(int time)
    {
        _timeToNextWave.text = $"Time To Next Wave: {time}";
    }

    private void SubscribeEvents()
    {
        try
        {
            Base.OnBaseHealthChanged += HandleBaseHealth;
            WaveManager.OnWaveCountChanged += HandleCurrentWave;
            EnemySpawnController.OnEnemySpawned += HandleAliveEnemyCount;
            LevelManager.OnRemainingTimeChanged += HandleRemainingTimeToNextWave;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error subscribing to events: " + e.Message);
        }
    }

    private void UnsubscribeEvents()
    {
        try
        {
            Base.OnBaseHealthChanged -= HandleBaseHealth;
            WaveManager.OnWaveCountChanged -= HandleCurrentWave;
            EnemySpawnController.OnEnemySpawned -= HandleAliveEnemyCount;
            LevelManager.OnRemainingTimeChanged -= HandleRemainingTimeToNextWave;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error unsubscribing from events: " + e.Message);
        }
    }
}