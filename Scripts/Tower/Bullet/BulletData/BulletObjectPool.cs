using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    public static BulletObjectPool Instance { get; private set; }

    [SerializeField] private BulletPool[] _pools = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializePools();
    }

    private void InitializePools()
    {
        for (int j = 0; j < _pools.Length; j++)
        {
            _pools[j].pooledBullets = new Queue<GameObject>();

            for (int i = 0; i < _pools[j].pooledBulletSize; i++)
            {
                GameObject obj = Instantiate(_pools[j].bulletPrefab);
                obj.SetActive(false);

                _pools[j].pooledBullets.Enqueue(obj);
            }
        }
    }

    public GameObject GetPooledBullet(BulletType bulletType)
    {
        int bulletTypeIndex = (int)bulletType;

        if (bulletTypeIndex >= _pools.Length)
        {
            return null;
        }

        GameObject obj = _pools[bulletTypeIndex].pooledBullets.Dequeue();
        obj.SetActive(true);

        _pools[bulletTypeIndex].pooledBullets.Enqueue(obj);

        return obj;
    }

    [System.Serializable]
    public struct BulletPool
    {
        public Queue<GameObject> pooledBullets;
        public GameObject bulletPrefab;
        public int pooledBulletSize;
    }

    public enum BulletType
    {
        StandartBullet,
        AOEBullet,
        ElectricBullet,
    }
}