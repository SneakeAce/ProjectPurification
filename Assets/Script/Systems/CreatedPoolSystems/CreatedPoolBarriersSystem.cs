using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolBarriersSystem : CreatedPoolSystem<PlaceableObject, BarriersType>
{
    private CreatedPoolBarrierConfig _config;

    public CreatedPoolBarriersSystem(CreatedPoolBarrierConfig config) : base(config)
    {
        _config = config;

        Initialization();
    }

    protected override void Initialization()
    {
        _poolDictionary = new Dictionary<BarriersType, ObjectPool<PlaceableObject>>();

        StartingCreatePools();
    }

    protected override void StartingCreatePools()
    {
        if (_config.PoolBarrierConfigs.Count > 0)
        {
            foreach (var config in _config.PoolBarrierConfigs)
            {
                if (_poolDictionary.ContainsKey(config.BarrierType))
                    continue;

                ObjectPool<PlaceableObject> pool = CreatePool(config.BarrierType, config.Prefab, config.MaxCountCurrentBarrierOnScene);
                _poolDictionary.Add(config.BarrierType, pool);
            }
        }
    }

    protected override ObjectPool<PlaceableObject> CreatePool(BarriersType barrierType, PlaceableObject placeableObject, int maxPoolSize)
    {
        ObjectPool<PlaceableObject> placeableObjectPool;

        GameObject newHolder = new GameObject(barrierType.ToString());
        newHolder.transform.SetParent(null);
        newHolder.transform.position = Vector3.zero;

        if (newHolder != null)
        {
            placeableObjectPool = new ObjectPool<PlaceableObject>(placeableObject, maxPoolSize, newHolder.transform);

            return placeableObjectPool;
        }

        return null;
    }
}
