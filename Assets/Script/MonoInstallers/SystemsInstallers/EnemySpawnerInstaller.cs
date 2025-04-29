using UnityEngine;
using Zenject;

public class EnemySpawnerInstaller : MonoInstaller
{
    [SerializeField] private GlobalEnemySpawnerConfig _globalSpawnerConfig;
    [SerializeField] private LocalEnemySpawnerConfig _localSpawnerConfig;

    public override void InstallBindings()
    {
        BindConfigs();

        BindEnemyFactory();

        CreateAndBindEnemySpawners();
    }

    private void BindEnemyFactory()
    {
        Container.Bind<IFactory<EnemyCharacter, EnemyConfig, EnemyType>>().To<EnemyFactory>().AsSingle();
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
