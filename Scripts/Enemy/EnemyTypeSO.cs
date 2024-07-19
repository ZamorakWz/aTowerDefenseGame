using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Enemy Type", menuName = "Enemy Type")]
public class EnemyTypeSO : ScriptableObject
{
    public GameObject typePrefab;
    public string typeName = "None";
    [SerializeField] private float _typeSpeed;
    [SerializeField] private float _typeMaxHealth;

    public float MaxHealth => _typeMaxHealth;
    public float Speed => _typeSpeed;
}
