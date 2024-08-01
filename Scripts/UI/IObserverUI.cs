using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserverUI
{
    void UpdateAliveEnemyCount(int count);
    void UpdateBaseHealth(int maxHealth, int currentHealth);
    void UpdateCurrentWave(int currentWave, int maxValue);
    void UpdateTimeToNextWave(int time);
}