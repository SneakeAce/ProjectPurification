using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnPointForSpawnerFactory : ISpawnPointFactory
{
    private IInstantiator _container;
    private SpawnPointForSpawnerConfigs _spawnPointsConfig;

    public SpawnPointForSpawnerFactory(IInstantiator container, SpawnPointForSpawnerConfigs pointConfig)
    {
        _container = container;
        _spawnPointsConfig = pointConfig;
    }

    public List<SpawnPointForSpawner> Create(Transform holder)
    {
        List<SpawnPointForSpawner> list = new List<SpawnPointForSpawner>();

        foreach (SpawnPointForSpawnerConfig config in _spawnPointsConfig.SpawnPointConfig)
        {
            SpawnPointForSpawner newPoint = _container.InstantiatePrefabForComponent<SpawnPointForSpawner>(config.SpawnPointPrefab);
            newPoint.Initialize(config);

            newPoint.transform.position = config.PositionPointOnScene;
            newPoint.transform.SetParent(holder, false);

            list.Add(newPoint);
        }

        return list;
    }
}
