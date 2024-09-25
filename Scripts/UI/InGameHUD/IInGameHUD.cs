using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInGameHUD
{
    void HandleAliveEnemyCount(int count);
    void HandleBaseHealth(int maxHealth, int currentHealth);
    void HandleCurrentWave(int currentWave, int maxValue);
    void HandleRemainingTimeToNextWave(int time);
    void HandleGoldUIUpdate(int amount);
}