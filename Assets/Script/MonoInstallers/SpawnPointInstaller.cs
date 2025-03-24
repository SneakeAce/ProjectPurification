using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnPointInstaller : MonoInstaller
{
    [SerializeField] private SpawnPointsConfig _spawnPointsConfig;

    public override void InstallBindings()
    {
        BindSpawnPointConfig();

        BindSpawnPointFactory();
    }

    private void BindSpawnPointConfig()
    {
        Container.Bind<SpawnPointsConfig>()
            .FromInstance(_spawnPointsConfig)
            .AsTransient()
            .NonLazy();
    }

    private void BindSpawnPointFactory()
    {
        Container.Bind<ISpawnPointFactory>()
            .To<SpawnPointFactory>()
            .AsSingle();
    }
}
