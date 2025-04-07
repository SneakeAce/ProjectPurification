using UnityEngine;
using Zenject;

public class CreatedPoolSystemInstaller : MonoInstaller
{
    [SerializeField] private CreatedPoolBarrierConfig _poolBarrierConfig;
    [SerializeField] private CreatedPoolBulletConfig _poolBulletConfig;
    [SerializeField] private CreatedPoolEnemyConfig _poolEnemyConfig;
    [SerializeField] private CreatedPoolTurretConfig _poolTurretConfig;

    public override void InstallBindings()
    {
        BindConfigs();

        BindCreatedPoolSystems();
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
        Container.Bind<CreatedPoolBarriersSystem>().AsTransient();
        Container.Bind<CreatedPoolBulletsSystem>().AsTransient();
        Container.Bind<CreatedPoolEnemiesSystem>().AsTransient();
        Container.Bind<CreatedPoolTurretsSystem>().AsTransient();
    }
}
