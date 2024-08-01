using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreationManager : MonoBehaviour
{
    #region Singleton
    public static TowerCreationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    //Tower prefabs and datas from scriptable objects
    [SerializeField] private GameObject _greenTowerPrefab;
    [SerializeField] private TowerTypeSO _greenTowerData;
    [SerializeField] private GameObject _blueTowerPrefab;
    [SerializeField] private TowerTypeSO _blueTowerData;
    [SerializeField] private GameObject _redTowerPrefab;
    [SerializeField] private TowerTypeSO _redTowerData;
    [SerializeField] private GameObject _whiteTowerPrefab;
    [SerializeField] private TowerTypeSO _whiteTowerData;

    public GameObject CreateTower(string towerType, Vector3 position)
    {
        GameObject towerPrefab = null;
        TowerTypeSO towerData = null;

        switch (towerType)
        {
            case "GreenTower":
                towerPrefab = _greenTowerPrefab;
                towerData = _greenTowerData;
                break;
            case "BlueTower":
                towerPrefab = _blueTowerPrefab;
                towerData = _blueTowerData;
                break;
            case "RedTower":
                towerPrefab = _redTowerPrefab;
                towerData = _redTowerData;
                break;
            case "WhiteTower":
                towerPrefab = _whiteTowerPrefab;
                towerData = _whiteTowerData;
                break;
            default:
                return null;
        }

        GameObject towerObject = Instantiate(towerPrefab, position, Quaternion.identity);
        AbstractTower abstractTower = towerObject.GetComponent<AbstractTower>();
        if (abstractTower != null)
        {
            abstractTower.Initialize(towerData);
        }

        return towerObject;
    }

    public List<AbstractTower> GetTowerList()
    {
        List<AbstractTower> abstractTowers = new List<AbstractTower>();
        if (_greenTowerPrefab != null) { abstractTowers.Add(_greenTowerPrefab.GetComponent<AbstractTower>()); }
        if (_blueTowerPrefab != null) { abstractTowers.Add(_blueTowerPrefab.GetComponent<AbstractTower>()); }
        if (_redTowerPrefab != null) { abstractTowers.Add(_redTowerPrefab.GetComponent<AbstractTower>()); }
        if (_whiteTowerPrefab != null) { abstractTowers.Add(_whiteTowerPrefab.GetComponent<AbstractTower>()); }
        return abstractTowers;
    }
}