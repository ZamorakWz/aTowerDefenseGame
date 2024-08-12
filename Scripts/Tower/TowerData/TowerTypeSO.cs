using Assets._Game.Scripts.Bullet;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Type", menuName = "Tower Type")]
public class TowerTypeSO : ScriptableObject
{
    public string towerName;
    public GameObject towerPrefab;
    public float towerCost;
    public float towerRange;
    public float towerDamage;
    public float towerFireRate;
    public float towerAOERadius;
    public BulletData towerBulletData;
}