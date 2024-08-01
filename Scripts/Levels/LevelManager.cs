using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static Action<int> OnRemainingTimeChanged;

    [SerializeField] private WaveManager _waveManager;
    [SerializeField] private int _timeBetweenWaves = 10;

    private void OnEnable()
    {
        WaveManager.OnWaveCompleted += HandleWaveEnd;
        WaveManager.OnAllWavesCompleted += HandleEndLevel;
    }

    private void Start()
    {
        StartLevel();
    }

    private void OnDisable()
    {
        WaveManager.OnWaveCompleted -= HandleWaveEnd;
        WaveManager.OnAllWavesCompleted -= HandleEndLevel;
    }

    public void StartLevel()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        _waveManager.StartWave();
        Debug.Log("Next Wave is Started!");
    }

    private void HandleWaveEnd()
    {
        StartCoroutine(NextWaveWithDelay());
        Debug.Log("Current Wave Completed!");
    }

    public void HandleEndLevel()
    {
        Debug.Log("Level Completed!");
    }

    private IEnumerator NextWaveWithDelay()
    {
        int remainingTime = _timeBetweenWaves;

        while (remainingTime >= 0)
        {
            OnRemainingTimeChanged?.Invoke(remainingTime);

            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        StartNextWave();
    }
}