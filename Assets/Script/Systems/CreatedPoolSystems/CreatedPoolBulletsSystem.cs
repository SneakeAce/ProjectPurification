using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolBulletsSystem : CreatedPoolSystem<Bullet, BulletType>
{
    private CreatedPoolBulletConfig _config;

    public CreatedPoolBulletsSystem(CreatedPoolBulletConfig config, ObjectPoolsHolder objectPoolsHolder) : base(config, objectPoolsHolder)
    {
        _config = config;

        Initialization();
    }

    protected override void Initialization()
    {
        _poolDictionary = new Dictionary<BulletType, ObjectPool<Bullet>>();

        StartingCreatePools();
    }

    protected override void StartingCreatePools()
    {
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
        newHolder.transform.SetParent(_objectPoolsHolder.transform);
        newHolder.transform.position = Vector3.zero;

        if (newHolder != null)
        {
            bulletObjectPool = new ObjectPool<Bullet>(bulletObject, maxPoolSize, newHolder.transform);

            return bulletObjectPool;
        }

        return null;
    }
}
