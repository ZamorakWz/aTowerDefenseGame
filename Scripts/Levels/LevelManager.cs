using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private WaveManager _waveManager;
    [SerializeField] private int _timeBetweenWaves = 10;

    private void OnEnable()
    {
        _waveManager.OnWaveCompleted += HandleWaveEnd;
        _waveManager.OnAllWavesCompleted += HandleEndLevel;
    }

    private void Start()
    {
        StartLevel();
    }

    private void OnDisable()
    {
        _waveManager.OnWaveCompleted -= HandleWaveEnd;
        _waveManager.OnAllWavesCompleted -= HandleEndLevel;
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
            InGameUIManager.Instance.UpdateTimeToNextWave(remainingTime);

            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        StartNextWave();
    }
}