using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Type", menuName = "Tower Type")]
public class TowerTypeSO : ScriptableObject
{
    public string towerTypeName;
    public GameObject towerTypePrefab;
    public float towerTypeCost;
    public float towerTypeRange;
    public float towerTypeDamage;
    public float towerTypeFireRate;
}