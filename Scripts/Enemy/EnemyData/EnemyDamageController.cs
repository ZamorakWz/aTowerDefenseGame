using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private EnemyTypeSO _enemyTypeSO;
    private EnemyHealthController _healthController;

    private void Start()
    {
        _healthController = GetComponent<EnemyHealthController>();
        _damage = _enemyTypeSO.typeDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null )
        {
            Debug.Log($"Base damaged! {gameObject.name}, {_damage}");
            damageable.TakeDamage(_damage);
            _healthController.Die();
        }
    }
}