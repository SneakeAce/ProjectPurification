using UnityEngine;
using Zenject;

public class CreatedPoolSystemInstaller : MonoInstaller
{
    [SerializeField] private ObjectPoolsHolder _holder;

    [SerializeField] private CreatedPoolBarrierConfig _poolBarrierConfig;
    [SerializeField] private CreatedPoolBulletConfig _poolBulletConfig;
    [SerializeField] private CreatedPoolEnemyConfig _poolEnemyConfig;
    [SerializeField] private CreatedPoolTurretConfig _poolTurretConfig;

    private Vector3 _holderSpawnCoordinates = new Vector3(0, 0, 0);

    public override void InstallBindings()
    {
        BindHolder();

        BindConfigs();

        BindCreatedPoolSystems();
    }

    private void BindHolder()
    {
        ObjectPoolsHolder holder = Container.InstantiatePrefabForComponent<ObjectPoolsHolder>(
            _holder,
            _holderSpawnCoordinates,
            Quaternion.identity,
            null);

        Container.Bind<ObjectPoolsHolder>().FromInstance(holder).AsSingle();
    }

    private void BindConfigs()
    {
        Container.Bind<CreatedPoolBarrierConfig>().FromInstance(_poolBarrierConfig).AsSingle();
        Container.Bind<CreatedPoolBulletConfig>().FromInstance(_poolBulletConfig).AsSingle();
        Container.Bind<CreatedPoolEnemyConfig>().FromInstance(_poolEnemyConfig).AsSingle();
        Container.Bind<CreatedPoolTurretConfig>().FromInstance(_poolTurretConfig).AsSingle();
    }

    private void BindCreatedPoolSystems()
    {
        Container.Bind<CreatedPoolBarriersSystem>().AsSingle();
        Container.Bind<CreatedPoolBulletsSystem>().AsSingle();
        Container.Bind<CreatedPoolEnemiesSystem>().AsSingle();
        Container.Bind<CreatedPoolTurretsSystem>().AsSingle();
    }
}
