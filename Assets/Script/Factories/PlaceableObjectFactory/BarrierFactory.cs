using UnityEngine;
using Zenject;

public class BarrierFactory : IFactory<Barrier, BarrierConfig, BarriersType>
{
    private DiContainer _container;

    private CreatedPoolBarriersSystem _barrierPools;

    private ConfigsLibrariesHandler<BarrierConfig, BarriersType> _handlerBarrrierConfigs;

    public BarrierFactory(DiContainer container, CreatedPoolBarriersSystem barrierPools, 
        ConfigsLibrariesHandler<BarrierConfig, BarriersType> handlerBarrierConfigs)
    {
        _container = container;

        _barrierPools = barrierPools;

        _handlerBarrrierConfigs = handlerBarrierConfigs;
    }

    public Barrier Create(Vector3 spawnPosition, BarriersType barrierType,
        Quaternion rotation)
    {
        ObjectPool<Barrier> barrierPool = GetPool(barrierType);

        if (barrierPool == null)
            return null;

        Barrier barrier = barrierPool.GetPoolObject();

        if (barrier == null)
            return null;

        BarrierConfig config = GetObjectConfig(barrierType);

        _container.Inject(barrier);

        barrier.SetPool(barrierPool);
        barrier.SetComponents(config);

        barrier.transform.position = spawnPosition;
        barrier.transform.rotation = rotation;

        return barrier;
    }

    public BarrierConfig GetObjectConfig(BarriersType type)
    {
        BarrierConfig config = _handlerBarrrierConfigs.GetObjectConfig(type);

        if (config == null)
            return null;

        return config;
    }

    private ObjectPool<Barrier> GetPool(BarriersType barrierType)
    {
        BarriersType barrierTypeSelected = barrierType;

        if (_barrierPools.PoolDictionary.TryGetValue(barrierTypeSelected, out ObjectPool<Barrier> poolSelected))
            return poolSelected;

        return null;
    }

}
