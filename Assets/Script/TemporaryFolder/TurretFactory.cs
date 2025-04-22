using UnityEngine;
using Zenject;

public class TurretFactory : IFactory<Turret, TurretType>
{
    private DiContainer _container;

    private CreatedPoolTurretsSystem _turretPools;

    private ConfigsLibrariesHandler<TurretConfig, TurretType> _handlerTurretConfigs;

    public TurretFactory(DiContainer container, CreatedPoolTurretsSystem barrierPools,
        ConfigsLibrariesHandler<TurretConfig, TurretType> handlerTurretConfigs)
    {
        _container = container;

        _turretPools = barrierPools;

        _handlerTurretConfigs = handlerTurretConfigs;
    }

    public Turret Create(Vector3 spawnPosition, TurretType barrierType,
        Quaternion rotation)
    {
        ObjectPool<Turret> turretPool = GetPool(barrierType);

        if (turretPool == null)
            return null;

        Turret turret = turretPool.GetPoolObject();

        if (turret == null)
            return null;

        TurretConfig config = GetBarrierConfig(barrierType);

        _container.Inject(turret);

        turret.SetComponents(config);

        turret.transform.position = spawnPosition;
        turret.transform.rotation = rotation;

        return turret;
    }

    private ObjectPool<Turret> GetPool(TurretType turretType)
    {
        TurretType turretTypeSelected = turretType;

        if (_turretPools.PoolDictionary.TryGetValue(turretTypeSelected, out ObjectPool<Turret> poolSelected))
            return poolSelected;

        return null;
    }

    private TurretConfig GetBarrierConfig(TurretType type)
    {
        TurretConfig config = _handlerTurretConfigs.GetObjectConfig(type);

        if (config == null)
            return null;

        return config;
    }

}
