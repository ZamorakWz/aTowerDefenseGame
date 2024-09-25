using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyObjectPool : MonoBehaviour
{
    [SerializeField] private Pool[] _pools = null;

    [Inject] private DiContainer _container;
    private void Awake()
    {
        for (int j = 0; j < _pools.Length; j++)
        {
            _pools[j]._pooledEnemies = new Queue<GameObject>();

            for (int i = 0; i < _pools[j]._pooledEnemySize; i++)
            {
                GameObject obj = Instantiate(_pools[j]._enemyPrefab);
                obj.SetActive(false);

                _pools[j]._pooledEnemies.Enqueue(obj);
            }
        }
    }

    public GameObject GetPooledEnemy(EnemyType enemyType)
    {
        int enemyTypeIndex = (int)enemyType;

        if (enemyTypeIndex >= _pools.Length)
        {
            return null;
        }

        GameObject obj = _pools[enemyTypeIndex]._pooledEnemies.Dequeue();
        obj.SetActive(true);

        obj.GetComponent<EnemyMovement>().SetEnemyWaypointIndexBeginingValue();
        obj.GetComponent<EnemyHealthController>().SetEnemyHealthBeginningValue();

        _pools[enemyTypeIndex]._pooledEnemies.Enqueue(obj);

        return obj;
    }

    [System.Serializable]
    public struct Pool
    {
        public Queue<GameObject> _pooledEnemies;
        public GameObject _enemyPrefab;
        public int _pooledEnemySize;
    }

    public enum EnemyType
    {
        GreenEnemy,
        BlueEnemy,
        RedEnemy,
        WhiteEnemy
    }
}
