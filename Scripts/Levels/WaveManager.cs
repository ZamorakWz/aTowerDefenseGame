using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{

    public static event Action OnWaveCompleted;
    public static event Action OnAllWavesCompleted;
    public static event Action<int, int> OnWaveCountChanged;
    public static Action<int> OnRemainingTimeChanged;

    [Header("How Many Waves For This Level")]
    [SerializeField] private List<WaveConfig> waveConfigs = new List<WaveConfig>();

    [SerializeField] private EnemySpawnController _enemySpawnController;
    [SerializeField] private int _currentWaveIndex = 0;
    [SerializeField] private int _totalTimeBetweenWaves = 10;

    private void OnEnable()
    {
        EnemySpawnController.OnAllEnemiesDefeated += HandleWaveCompleted;
    }

    private void OnDisable()
    {
        EnemySpawnController.OnAllEnemiesDefeated -= HandleWaveCompleted;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartWave();
        }
    }

    public void StartWave()
    {
        if (_currentWaveIndex < waveConfigs.Count)
        {
            StartCoroutine(SpawnEnemiesForWave(waveConfigs[_currentWaveIndex]));
            _currentWaveIndex++;
            OnWaveCountChanged?.Invoke(waveConfigs.Count, _currentWaveIndex);
        }
        else
        {
            OnAllWavesCompleted.Invoke();
            Debug.Log("All Waves are Defeated!");
        }
    }

    private void HandleWaveCompleted()
    {
        OnWaveCompleted?.Invoke();
        NextWaveWithDelayAsync().Forget();
        Debug.Log("Current Wave Completed!");
    }

    private async UniTaskVoid NextWaveWithDelayAsync()
    {
        int remainingTime = _totalTimeBetweenWaves;

        while (remainingTime >= 0)
        {
            OnRemainingTimeChanged?.Invoke(remainingTime);

            await UniTask.Delay(1000);
            remainingTime--;
        }

        StartWave();
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