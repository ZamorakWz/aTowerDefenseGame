using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{

    public event Action OnWaveCompleted;
    public event Action OnAllWavesCompleted;

    [Header("How Many Waves For This Level")]
    [SerializeField] private List<WaveConfig> waveConfigs = new List<WaveConfig>();
    [SerializeField] private EnemySpawnController _enemySpawnController;

    [SerializeField] private int _currentWaveIndex = 0;

    private void OnEnable()
    {
        _enemySpawnController.OnAllEnemiesDefeated += HandleWaveCompleted;
    }

    private void OnDisable()
    {
        _enemySpawnController.OnAllEnemiesDefeated -= HandleWaveCompleted;
    }

    public void StartWave()
    {
        if (_currentWaveIndex < waveConfigs.Count)
        {
            StartCoroutine(SpawnEnemiesForWave(waveConfigs[_currentWaveIndex]));
            _currentWaveIndex++;
            InGameUIManager.Instance.UpdateCurrentWave(waveConfigs.Count, _currentWaveIndex);
        }
        else
        {
            OnAllWavesCompleted.Invoke();
            Debug.Log("All Waves are Defeated!");
        }
    }

    private void HandleWaveCompleted()
    {
        OnWaveCompleted.Invoke();
    }

    private IEnumerator SpawnEnemiesForWave(WaveConfig waveConfig)
    {
        foreach (var config in waveConfig.enemyConfigs)
        {
            yield return StartCoroutine(_enemySpawnController.SpawnEnemyRoutine(config));
        }
    }

    [System.Serializable]
    public class WaveConfig
    {
        [Header("How Many and What Type Enemies for This Wave")]
        public List<EnemySpawnController.EnemySpawnConfig> enemyConfigs;
    }
}