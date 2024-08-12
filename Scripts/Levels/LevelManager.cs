using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    private void OnEnable()
    {
        WaveManager.OnAllWavesCompleted += HandleEndLevel;
    }

    private void OnDisable()
    {
        WaveManager.OnAllWavesCompleted -= HandleEndLevel;
    }

    public void HandleEndLevel()
    {
        Debug.Log("Level Completed!");
    }


}