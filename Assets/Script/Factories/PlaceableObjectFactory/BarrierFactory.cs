using UnityEngine;
using Zenject;

public class BarrierFactory : IFactory<PlaceableObject, BarriersType>
{
    private DiContainer _container;

    private CreatedPoolBarriersSystem _barrierPools;

    private ConfigsLibrariesHandler<PlaceableObjectConfig, BarriersType> _handlerBarrrierConfigs;

    public BarrierFactory(DiContainer container, CreatedPoolBarriersSystem barrierPools, 
        ConfigsLibrariesHandler<PlaceableObjectConfig, BarriersType> handlerBarrierConfigs)
    {
        _container = container;

        _barrierPools = barrierPools;

        _handlerBarrrierConfigs = handlerBarrierConfigs;
    }

    public PlaceableObject Create(Vector3 spawnPosition, BarriersType barrierType,
        Quaternion rotation)
    {
        ObjectPool<PlaceableObject> placeableObejctPool = GetPool(barrierType);

        if (placeableObejctPool == null)
            return null;

        PlaceableObject placeableObject = placeableObejctPool.GetPoolObject();

        if (placeableObject == null)
            return null;

        PlaceableObjectConfig config = GetBarrierConfig(barrierType);

        _container.Inject(placeableObject);

        placeableObject.SetComponents(config);

        placeableObject.transform.position = spawnPosition;
        placeableObject.transform.rotation = rotation;

        return placeableObject;
    }

    private ObjectPool<PlaceableObject> GetPool(BarriersType barrierType)
    {
        BarriersType barrierTypeSelected = barrierType;

        if (_barrierPools.PoolDictionary.TryGetValue(barrierTypeSelected, out ObjectPool<PlaceableObject> poolSelected))
            return poolSelected;

        return null;
    }

    private PlaceableObjectConfig GetBarrierConfig(BarriersType type)
    {
        PlaceableObjectConfig config = _handlerBarrrierConfigs.GetObjectConfig(type);

        if (config == null)
            return null;

        return config;
    }
}
