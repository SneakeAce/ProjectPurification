using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolTurretsSystem : CreatedPoolSystem<Turret, TurretType>
{

    private CreatedPoolTurretConfig _config;

    public CreatedPoolTurretsSystem(CreatedPoolTurretConfig config, ObjectPoolsHolder objectPoolsHolder) : base(config, objectPoolsHolder)
    {
        _config = config;

        Initialization();
    }

    protected override void Initialization()
    {
        _poolDictionary = new Dictionary<TurretType, ObjectPool<Turret>>();

        StartingCreatePools();
    }

    protected override void StartingCreatePools()
    {
        if (_config.PoolTurretsConfigs.Count > 0)
        {
            foreach (var config in _config.PoolTurretsConfigs)
            {
                if (_poolDictionary.ContainsKey(config.TurretType))
                    continue;

                ObjectPool<Turret> pool = CreatePool(config.TurretType, config.Prefab, config.MaxCountCurrentTurretOnScene);
                _poolDictionary.Add(config.TurretType, pool);
            }
        }
    }

    protected override ObjectPool<Turret> CreatePool(TurretType turretType, Turret turretObject, int maxPoolSize)
    {
        ObjectPool<Turret> turretObjectPool;

        GameObject newHolder = new GameObject(turretType.ToString());
        newHolder.transform.SetParent(_objectPoolsHolder.transform);
        newHolder.transform.position = Vector3.zero;

        if (newHolder != null)
        {
            turretObjectPool = new ObjectPool<Turret>(turretObject, maxPoolSize, newHolder.transform);

            return turretObjectPool;
        }

        return null;
    }
}
