using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SphereTargetDetector : MonoBehaviour, ITargetDetector
{
    private SphereCollider _detectionCollider;
    [SerializeField] private List<IAttackable> _targetsInRange = new List<IAttackable>();

    private void OnEnable()
    {
        EnemyHealthController.OnEnemyDied += HandleEnemyDied;
    }

    private void OnDisable()
    {
        EnemyHealthController.OnEnemyDied -= HandleEnemyDied;
    }

    public void InitializeTargetDetector(float detectionRadius)
    {
        _detectionCollider = gameObject.GetComponent<SphereCollider>();
        _detectionCollider.radius = detectionRadius;
        _detectionCollider.isTrigger = true;
    }

    public List<IAttackable> GetTargetsInRange()
    {
        return _targetsInRange;
    }

    public void AddTarget(IAttackable target)
    {
        if (!_targetsInRange.Contains(target))
        {
            _targetsInRange.Add(target);
        }
    }

    public void RemoveTarget(IAttackable target)
    {
        if (_targetsInRange.Contains(target))
        {
            _targetsInRange.Remove(target);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IAttackable attackable = other.GetComponent<IAttackable>();
        if (attackable != null)
        {
            AddTarget(attackable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IAttackable attackable = other.GetComponent<IAttackable>();
        if (attackable != null)
        {
            RemoveTarget(attackable);
        }
    }

    private void HandleEnemyDied(EnemyHealthController enemy)
    {
        RemoveTarget(enemy);
    }
}
