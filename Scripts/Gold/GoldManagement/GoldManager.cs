using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;

    public static event Action<int> OnGoldChanged;

    private int _currentGold = 100;

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

    public void AddGold(int amount)
    {
        _currentGold += amount;

        OnGoldChanged?.Invoke(_currentGold);
    }

    public void RemoveGold(int amount)
    {
        _currentGold -= amount;

        OnGoldChanged?.Invoke(_currentGold);
    }

    public int GetCurrentGold()
    {
        return _currentGold;
    }
}