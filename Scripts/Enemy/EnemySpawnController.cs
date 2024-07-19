using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySpawnController;

public class EnemySpawnController : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnConfig
    {
        [Header("Enemy Type That Will Spawn GreenEnemy/BlueEnemy/RedEnemy/WhiteEnemy")]
        public EnemyObjectPool.EnemyType enemyType;
        [Header("Enemy Count On Level")]
        public int spawnCount;
        [Header("Enemies Spawn Interval")]
        public float spawnInterval;
    }

    [Header("Spawn Position of Enemy")]
    [SerializeField] private Vector3 _spawnPosition;

    [SerializeField] private EnemyObjectPool _enemyObjectPool;

    [SerializeField] private EnemySpawnConfig[] _enemySpawnConfig;

    public IEnumerator SpawnEnemyRoutine(EnemySpawnConfig config)
    {
        int counter = 0;
        while (counter < config.spawnCount)
        {
            GameObject obj = _enemyObjectPool.GetPooledEnemy(config.enemyType);

            if (obj != null)
            {
                obj.transform.position = _spawnPosition;
                counter++;
            }

            yield return new WaitForSeconds(config.spawnInterval);
        }
    }
}
