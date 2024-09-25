using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IPositionProvider, ISpeedProvider
{
    private EnemyWaypointPath _waypointPath;

    private Transform _targetWaypoint;
    [SerializeField] private int _currentWaypointIndex = 0;
    public int CurrentWaypointIndex
    {  
        get { return _currentWaypointIndex; }
        set { _currentWaypointIndex = Mathf.Max(0, value); }
    }

    private Vector3 _currentDirection;

    private float _speed;
    [SerializeField] private EnemyTypeSO _enemyType;

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    void Start()
    {
        _waypointPath = FindObjectOfType<EnemyWaypointPath>();

        Speed = _enemyType.typeSpeed;
    }

    void Update()
    {
        MoveTowardsWaypoints();
    }

    private void MoveTowardsWaypoints()
    {
        if (_currentWaypointIndex < _waypointPath._enemyWaypoints.Length)
        {
            _targetWaypoint = _waypointPath._enemyWaypoints[_currentWaypointIndex];
            gameObject.transform.LookAt(_targetWaypoint);
            _currentDirection = _targetWaypoint.position - transform.position;
            transform.Translate(_currentDirection.normalized * _speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, _targetWaypoint.position) < 0.1f)
            {
                _currentWaypointIndex++;
            }
        }
    }

    public void SetEnemyWaypointIndexBeginingValue()
    {
        CurrentWaypointIndex = 0;
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public float GetSpeedValue()
    {
        return Speed;
    }
}