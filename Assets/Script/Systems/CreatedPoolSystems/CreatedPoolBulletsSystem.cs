using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolBulletsSystem : CreatedPoolSystem<Bullet, BulletType>
{
    public CreatedPoolBulletsSystem(CreatedPoolBulletConfig config) : base(config)
    {
        Initialization();
    }

    protected override void Initialization()
    {
        _poolDictionary = new Dictionary<BulletType, ObjectPool<Bullet>>();

        StartingCreatePools();
    }

    protected override void StartingCreatePools()
    {
        if (_monoObjects.Count > 0)
        {
            foreach (Bullet bulletObject in _monoObjects)
            {
                if (_poolDictionary.ContainsKey(bulletObject.BulletType))
                    continue;

                switch (bulletObject.BulletType)
                {
                    case BulletType.PistolBullet:
                        ObjectPool<Bullet> poolAutomaticTurret = CreatePool(bulletObject.BulletType, bulletObject, bulletObject.MaxCountOnScene);
                        _poolDictionary.Add(bulletObject.BulletType, poolAutomaticTurret);
                        break;

                    case BulletType.RifleBullet:
                        ObjectPool<Bullet> poolMachineGunTurret = CreatePool(bulletObject.BulletType, bulletObject, bulletObject.MaxCountOnScene);
                        _poolDictionary.Add(bulletObject.BulletType, poolMachineGunTurret);

                        break;

                    case BulletType.MachineGunBullet:
                        ObjectPool<Bullet> poolArmorPiercingTurret = CreatePool(bulletObject.BulletType, bulletObject, bulletObject.MaxCountOnScene);
                        _poolDictionary.Add(bulletObject.BulletType, poolArmorPiercingTurret);

                        break;

                    default:
                        throw new ArgumentException("This Bullet type does not exist");
                }
            }
        }
    }

    protected override ObjectPool<Bullet> CreatePool(BulletType bulletType, Bullet bulletObject, int maxPoolSize)
    {
        ObjectPool<Bullet> turretObjectPool;

        GameObject newHolder = new GameObject(bulletType.ToString());
        newHolder.transform.SetParent(null);
        newHolder.transform.position = Vector3.zero;

        if (newHolder != null)
        {
            turretObjectPool = new ObjectPool<Bullet>(bulletObject, maxPoolSize, newHolder.transform);

            return turretObjectPool;
        }

        return null;
    }
}
