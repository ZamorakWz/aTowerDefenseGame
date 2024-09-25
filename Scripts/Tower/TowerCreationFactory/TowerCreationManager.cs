using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreationManager : MonoBehaviour
{
    public static TowerCreationManager Instance {  get; private set; }

    public static Action<List<AbstractBaseTower>> OnGetTowerList;

    [SerializeField] private List<TowerInfo> towerInfoList;

    private Dictionary<string, ITowerFactory> towerFactories;

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

        InitializeTowerFactories();
    }

    private void Start()
    {
        GetTowerList();
    }

    private void InitializeTowerFactories()
    {
        towerFactories = new Dictionary<string, ITowerFactory>();
        foreach (var towerInfo in towerInfoList)
        {
            towerFactories[towerInfo.towerType] = new ConcreteTowerFactory(towerInfo.towerPrefab, towerInfo.towerData);
        }
    }

    public GameObject CreateTower(string towerType, Vector3 position)
    {
        if (towerFactories.TryGetValue(towerType, out ITowerFactory factory))
        {
            AbstractBaseTower tower = factory.CreateTower(position);
            return tower?.gameObject;
        }
        return null;
    }

    public List<AbstractBaseTower> GetTowerList()
    {
        List<AbstractBaseTower> abstractBaseTowers = new List<AbstractBaseTower>();

        foreach (var towerInfo in towerInfoList)
        {
            AbstractBaseTower tower = towerInfo.towerPrefab.GetComponent<AbstractBaseTower>();
            if (tower != null)
            {
                abstractBaseTowers.Add(tower);
            }
        }

        OnGetTowerList.Invoke(abstractBaseTowers);
        return abstractBaseTowers;
    }

    [System.Serializable]
    private class TowerInfo
    {
        public string towerType;
        public GameObject towerPrefab;
        public TowerTypeSO towerData;
    }
}
