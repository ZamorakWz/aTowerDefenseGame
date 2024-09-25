using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Enemy Type", menuName = "Enemy Type")]
public class EnemyTypeSO : ScriptableObject
{
    public GameObject typePrefab;
    public string typeName = "None";
    public float typeSpeed;
    public float typeMaxHealth;
    public int typeDamage;
    public int typeGold;
}
