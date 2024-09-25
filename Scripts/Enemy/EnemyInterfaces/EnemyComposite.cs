using UnityEngine;

public class EnemyComposite : MonoBehaviour, IAttackable, IPositionProvider, IHealthProvider
{
    private EnemyHealthController _healthController;
    private EnemyMovement _movement;

    void Awake()
    {
        _healthController = GetComponent<EnemyHealthController>();
        _movement = GetComponent<EnemyMovement>();
    }

    public void TakeDamage(float amount)
    {
        _healthController?.TakeDamage(amount);
    }

    public Vector3 GetPosition()
    {
        return _movement?.GetPosition() ?? Vector3.zero;
    }

    public float GetHealth()
    {
        return _healthController?.GetHealth() ?? 0f;
    }
}