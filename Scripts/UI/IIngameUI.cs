using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIngameUI
{
    void HandleAliveEnemyCount(int count);
    void HandleBaseHealth(int maxHealth, int currentHealth);
    void HandleCurrentWave(int currentWave, int maxValue);
    void HandleRemainingTimeToNextWave(int time);
}