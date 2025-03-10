using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CreatedPoolSystemInstaller : MonoInstaller
{
    [SerializeField] private CreatedPoolBarrirerConfig _poolBarrierConfig;
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
        Container.Bind<CreatedPoolBarrirerConfig>().FromInstance(_poolBarrierConfig).AsSingle().NonLazy();
        Container.Bind<CreatedPoolBulletConfig>().FromInstance(_poolBulletConfig).AsSingle().NonLazy();
        Container.Bind<CreatedPoolEnemyConfig>().FromInstance(_poolEnemyConfig).AsSingle().NonLazy();
        Container.Bind<CreatedPoolTurretConfig>().FromInstance(_poolTurretConfig).AsSingle().NonLazy();
    }

    private void BindCreatedPoolSystems()
    {
        Container.Bind<CreatedPoolBarriersSystem>().AsTransient().NonLazy();
        Container.Bind<CreatedPoolBulletsSystem>().AsTransient().NonLazy();
        Container.Bind<CreatedPoolEnemiesSystem>().AsTransient().NonLazy();
        Container.Bind<CreatedPoolBulletsSystem>().AsTransient().NonLazy();
    }
}
