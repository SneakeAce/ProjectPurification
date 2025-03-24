using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class EnemySpawnerInstaller : MonoInstaller
{
    [SerializeField] private GlobalEnemySpawnerConfig _globalSpawnerConfig;
    [SerializeField] private LocalEnemySpawnerConfig _localSpawnerConfig;

    public override void InstallBindings()
    {
        BindConfigs();

        CreateAndBindEnemySpawners();
    }

    private void BindConfigs()
    {
        Container.Bind<GlobalEnemySpawnerConfig>()
            .FromInstance(_globalSpawnerConfig)
            .AsTransient()
            .NonLazy();

        Container.Bind<LocalEnemySpawnerConfig>()
            .FromInstance(_localSpawnerConfig)
            .AsTransient()
            .NonLazy();
    }

    private void CreateAndBindEnemySpawners()
    {
        GlobalEnemySpawner globalSpawner = Container
            .InstantiatePrefabForComponent<GlobalEnemySpawner>(_globalSpawnerConfig.SpawnerPrefab,
            _globalSpawnerConfig.PositionSpawner, Quaternion.identity, null);

        LocalEnemySpawner localSpawner = Container
            .InstantiatePrefabForComponent<LocalEnemySpawner>(_localSpawnerConfig.SpawnerPrefab,
            _localSpawnerConfig.PositionSpawner, Quaternion.identity, null);

        Container.Bind<GlobalEnemySpawner>().FromInstance(globalSpawner).AsSingle().NonLazy();
        Container.Bind<LocalEnemySpawner>().FromInstance(localSpawner).AsSingle().NonLazy();
    }

}
