using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class SphereTargetDetector : MonoBehaviour, ITargetDetector
{
    private SphereCollider _detectionCollider;
    //[SerializeField] private List<IAttackable> _targetsInRange = new List<IAttackable>();
    [SerializeField] private HashSet<IAttackable> _targetsInRange = new HashSet<IAttackable>();

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
        _detectionCollider = gameObject.GetComponentInChildren<SphereCollider>();
        _detectionCollider.radius = detectionRadius;
        _detectionCollider.isTrigger = true;
    }

    public void UpdateRange(float newRange)
    {
        _detectionCollider.radius = newRange;
    }

    public List<IAttackable> GetTargetsInRange()
    {
        //return _targetsInRange;
        return new List<IAttackable>(_targetsInRange);
    }

    public void AddTarget(IAttackable target)
    {
        //if (!_targetsInRange.Contains(target))
        //{
        //    _targetsInRange.Add(target);
        //}

        _targetsInRange.Add(target);
    }

    public void RemoveTarget(IAttackable target)
    {
        //if (_targetsInRange.Contains(target))
        //{
        //    _targetsInRange.Remove(target);
        //}

        _targetsInRange.Remove(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyComposite target = other.GetComponent<EnemyComposite>();
        if (target != null)
        {
            AddTarget(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyComposite target = other.GetComponent<EnemyComposite>();
        if (target != null)
        {
            RemoveTarget(target);
        }
    }

    private void HandleEnemyDied(EnemyHealthController enemy)
    {
        EnemyComposite target = enemy.GetComponent<EnemyComposite>();
        if (target != null)
        {
            RemoveTarget(target);
        }
    }
}