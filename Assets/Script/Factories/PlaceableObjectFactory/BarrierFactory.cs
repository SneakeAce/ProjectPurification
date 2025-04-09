using UnityEngine;
using Zenject;

public class BarrierFactory : IBarrierFactory
{
    private DiContainer _container;

    private CreatedPoolBarriersSystem _enemiesPools;

    private ConfigsLibrariesHandler<PlaceableObjectConfig, BarriersType> _handlerEnemyConfigs;

    public BarrierFactory(DiContainer container, CreatedPoolBarriersSystem enemiesPools, 
        ConfigsLibrariesHandler<PlaceableObjectConfig, BarriersType> handlerBarrierConfigs)
    {
        _container = container;

        _enemiesPools = enemiesPools;

        _handlerEnemyConfigs = handlerBarrierConfigs;
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
        BarriersType enemyTypeSelected = barrierType;

        if (_enemiesPools.PoolDictionary.TryGetValue(enemyTypeSelected, out ObjectPool<PlaceableObject> poolSelected))
            return poolSelected;

        return null;
    }

    private PlaceableObjectConfig GetBarrierConfig(BarriersType type)
    {
        PlaceableObjectConfig config = _handlerEnemyConfigs.GetObjectConfig(type);

        if (config == null)
            return null;

        return config;
    }
}
