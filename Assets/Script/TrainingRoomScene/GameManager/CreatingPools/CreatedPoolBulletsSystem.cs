using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolBulletsSystem
{
    private List<Bullet> _bulletObjects;

    private Dictionary<BulletType, ObjectPool<Bullet>> _poolDictionary;

    public CreatedPoolBulletsSystem(IEnumerable<Bullet> bulletObjects)
    {
        _bulletObjects = new List<Bullet>(bulletObjects);
    }

    public Dictionary<BulletType, ObjectPool<Bullet>> PoolDictionary => _poolDictionary;

    public void Initialization()
    {
        _poolDictionary = new Dictionary<BulletType, ObjectPool<Bullet>>();

        StartingCreatePools();
    }

    private void StartingCreatePools()
    {
        if (_bulletObjects.Count > 0)
        {
            foreach (Bullet bulletObject in _bulletObjects)
            {
                if (_poolDictionary.ContainsKey(bulletObject.BulletType))
                    continue;

                switch (bulletObject.BulletType)
                {
                    case BulletType.PistolBullet:
                        ObjectPool<Bullet> poolAutomaticTurret = CreatePool(bulletObject.BulletType, bulletObject.MaxCountOnScene, bulletObject);
                        _poolDictionary.Add(bulletObject.BulletType, poolAutomaticTurret);
                        break;

                    case BulletType.RifleBullet:
                        ObjectPool<Bullet> poolMachineGunTurret = CreatePool(bulletObject.BulletType, bulletObject.MaxCountOnScene, bulletObject);
                        _poolDictionary.Add(bulletObject.BulletType, poolMachineGunTurret);

                        break;

                    case BulletType.MachineGunBullet:
                        ObjectPool<Bullet> poolArmorPiercingTurret = CreatePool(bulletObject.BulletType, bulletObject.MaxCountOnScene, bulletObject);
                        _poolDictionary.Add(bulletObject.BulletType, poolArmorPiercingTurret);

                        break;

                    default:
                        throw new ArgumentException("This turret type does not exist");
                }
            }
        }
    }

    private ObjectPool<Bullet> CreatePool(BulletType bulletType, int maxPoolSize, Bullet bulletObject)
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
