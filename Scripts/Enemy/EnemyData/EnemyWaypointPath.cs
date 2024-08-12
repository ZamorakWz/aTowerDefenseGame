using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaypointPath : MonoBehaviour
{
    public Transform[] _enemyWaypoints;

    private void OnDrawGizmos()
    {
        if (_enemyWaypoints != null && _enemyWaypoints.Length > 1)
        {
            for (int i = 0; i < _enemyWaypoints.Length - 1 ; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(_enemyWaypoints[i].position, _enemyWaypoints[i + 1].position);
            }
        }
    }
}