using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawnerInstaller : MonoInstaller
{
    [SerializeField] private GlobalEnemySpawnerConfig _globalSpawnerConfig;
    [SerializeField] private LocalEnemySpawnerConfig _localSpawnerConfig;

    [SerializeField] private SpawnPointConfig _spawnPointConfig;

    public override void InstallBindings()
    {
        BindConfigs();



    }

    private void BindConfigs()
    {
        Container.Bind<GlobalEnemySpawnerConfig>().FromInstance(_globalSpawnerConfig).AsTransient().NonLazy();
        Container.Bind<LocalEnemySpawnerConfig>().FromInstance(_localSpawnerConfig).AsTransient().NonLazy();

        Container.Bind<SpawnPointConfig>().FromInstance(_spawnPointConfig).AsTransient().NonLazy();
    }

    private void CreateAndBindSpawnPoint()
    {

    }
}
