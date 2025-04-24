using UnityEngine;
using Zenject;

public class BulletFactory : IFactory<Bullet, BulletConfig, BulletType>
{
    private DiContainer _container;

    private CreatedPoolBulletsSystem _bulletPools;

    private ConfigsLibrariesHandler<BulletConfig, BulletType> _handlerBulletConfigs;

    public BulletFactory(DiContainer container, CreatedPoolBulletsSystem bulletPools,
        ConfigsLibrariesHandler<BulletConfig, BulletType> handlerBulletConfigs)
    {
        _container = container;

        _bulletPools = bulletPools;

        _handlerBulletConfigs = handlerBulletConfigs;
    }

    public Bullet Create(Vector3 spawnPosition, BulletType bulletType,
        Quaternion rotation)
    {

        //Debug.Log("BulletFactory / Bullet Create ");
        ObjectPool<Bullet> bulletPool = GetPool(bulletType);

        if (bulletPool == null)
            return null;

        Bullet bullet = bulletPool.GetPoolObject();

        if (bullet == null)
            return null;

        BulletConfig config = GetObjectConfig(bulletType);

        _container.Inject(bullet);

        bullet.SetComponents(config);
        bullet.SetPool(bulletPool);

        bullet.transform.position = spawnPosition;
        bullet.transform.rotation = rotation;

        return bullet;
    }

    public BulletConfig GetObjectConfig(BulletType type)
    {
        BulletConfig config = _handlerBulletConfigs.GetObjectConfig(type);

        if (config == null)
            return null;

        return config;
    }

    private ObjectPool<Bullet> GetPool(BulletType bulletType)
    {
        BulletType bulletTypeSelected = bulletType;

        if (_bulletPools.PoolDictionary.TryGetValue(bulletTypeSelected, out ObjectPool<Bullet> poolSelected))
            return poolSelected;

        return null;
    }
}

