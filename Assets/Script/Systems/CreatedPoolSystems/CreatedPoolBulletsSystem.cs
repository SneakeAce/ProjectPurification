using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolBulletsSystem : CreatedPoolSystem<Bullet, BulletType>
{
    private CreatedPoolBulletConfig _config;

    public CreatedPoolBulletsSystem(CreatedPoolBulletConfig config) : base(config)
    {
        _config = config;

        Debug.Log("CreatedPoolBulletSystem Construct");

        Initialization();
    }

    protected override void Initialization()
    {
        _poolDictionary = new Dictionary<BulletType, ObjectPool<Bullet>>();

        Debug.Log("CreatedPoolBulletSystem Initialization");

        StartingCreatePools();
    }

    protected override void StartingCreatePools()
    {
        Debug.Log("Created Pool Bullets / _configs = " + _config.Objects.Count);
        if (_config.PoolBulletConfigs.Count > 0)
        {
            foreach (var config in _config.PoolBulletConfigs)
            {
                if (_poolDictionary.ContainsKey(config.BulletType))
                    continue;

                
                ObjectPool<Bullet> pool = CreatePool(config.BulletType, config.Prefab, config.MaxCountCurrentBulletOnScene);
                _poolDictionary.Add(config.BulletType, pool);
            }
        }
    }

    protected override ObjectPool<Bullet> CreatePool(BulletType bulletType, Bullet bulletObject, int maxPoolSize)
    {
        ObjectPool<Bullet> bulletObjectPool;

        GameObject newHolder = new GameObject(bulletType.ToString());
        newHolder.transform.SetParent(null);
        newHolder.transform.position = Vector3.zero;

        if (newHolder != null)
        {
            bulletObjectPool = new ObjectPool<Bullet>(bulletObject, maxPoolSize, newHolder.transform);

            return bulletObjectPool;
        }

        return null;
    }
}
