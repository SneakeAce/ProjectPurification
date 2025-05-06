using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolBarriersSystem : CreatedPoolSystem<Barrier, BarriersType>
{
    private CreatedPoolBarrierConfig _config;

    public CreatedPoolBarriersSystem(CreatedPoolBarrierConfig config, ObjectPoolsHolder objectPoolsHolder) : base(config, objectPoolsHolder)
    {
        _config = config;

        Initialization();
    }

    protected override void Initialization()
    {
        _poolDictionary = new Dictionary<BarriersType, ObjectPool<Barrier>>();

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

                ObjectPool<Barrier> pool = CreatePool(config.BarrierType, config.Prefab, config.MaxCountCurrentBarrierOnScene);
                _poolDictionary.Add(config.BarrierType, pool);
            }
        }
    }

    protected override ObjectPool<Barrier> CreatePool(BarriersType barrierType, Barrier barrier, int maxPoolSize)
    {
        ObjectPool<Barrier> barrierPool;

        GameObject newHolder = new GameObject(barrierType.ToString());
        newHolder.transform.SetParent(_objectPoolsHolder.transform);
        newHolder.transform.position = Vector3.zero;

        if (newHolder != null)
        {
            barrierPool = new ObjectPool<Barrier>(barrier, maxPoolSize, newHolder.transform);

            return barrierPool;
        }

        return null;
    }
}
