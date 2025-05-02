using UnityEngine;
using Zenject;

public class EnemySpawnerInstaller : MonoInstaller
{
    [SerializeField] private GlobalEnemySpawnerConfig _globalSpawnerConfig;
    [SerializeField] private LocalEnemySpawnerConfig _localSpawnerConfig;

    [SerializeField] private SpawnPointForSpawnerConfigs _spawnPointsForSpawnerConfig;

    public override void InstallBindings()
    {
        BindSpawnPointConfig();

        BindSpawnerConfigs();

        CreateAndBindEnemySpawners();
    }

    private void BindSpawnPointConfig()
    {
        Container.Bind<SpawnPointForSpawnerConfigs>()
            .FromInstance(_spawnPointsForSpawnerConfig)
            .AsTransient();
    }

    private void BindSpawnerConfigs()
    {
        Container.Bind<GlobalEnemySpawnerConfig>()
            .FromInstance(_globalSpawnerConfig)
            .AsTransient();

        Container.Bind<LocalEnemySpawnerConfig>()
            .FromInstance(_localSpawnerConfig)
            .AsTransient();
    }

    private void CreateAndBindEnemySpawners()
    {
        GlobalEnemySpawner globalSpawner = Container
            .InstantiatePrefabForComponent<GlobalEnemySpawner>(_globalSpawnerConfig.SpawnerPrefab,
            _globalSpawnerConfig.PositionSpawner, Quaternion.identity, null);

        LocalEnemySpawner localSpawner = Container
            .InstantiatePrefabForComponent<LocalEnemySpawner>(_localSpawnerConfig.SpawnerPrefab,
            _localSpawnerConfig.PositionSpawner, Quaternion.identity, null);

        Container.Bind<GlobalEnemySpawner>().FromInstance(globalSpawner).AsSingle();
        Container.Bind<LocalEnemySpawner>().FromInstance(localSpawner).AsSingle();
    }
}
