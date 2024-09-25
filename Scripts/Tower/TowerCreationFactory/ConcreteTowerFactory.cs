using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteTowerFactory : ITowerFactory
{
    private GameObject _towerPrefab;
    private TowerTypeSO _towerData;

    public ConcreteTowerFactory (GameObject towerPrefab, TowerTypeSO towerData)
    {
        _towerPrefab = towerPrefab;
        _towerData = towerData;
    }

    public AbstractBaseTower CreateTower(Vector3 position)
    {
        GameObject towerObject = UnityEngine.Object.Instantiate (_towerPrefab, position, Quaternion.identity);
        AbstractBaseTower tower = towerObject.GetComponent<AbstractBaseTower>();
        return tower;
    }
}