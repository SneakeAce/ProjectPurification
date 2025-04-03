using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnPointFactory : ISpawnPointFactory
{
    private IInstantiator _container;
    private SpawnPointsConfig _spawnPointsConfig;

    public SpawnPointFactory(IInstantiator container, SpawnPointsConfig pointConfig)
    {
        _container = container;
        _spawnPointsConfig = pointConfig;
    }

    public List<SpawnPoint> Create(Transform holder)
    {
        List<SpawnPoint> list = new List<SpawnPoint>();

        foreach (SpawnPointConfig config in _spawnPointsConfig.SpawnPointConfig)
        {
            SpawnPoint newPoint = _container.InstantiatePrefabForComponent<SpawnPoint>(config.SpawnPointPrefab);
            newPoint.Initialize(config);

            newPoint.transform.position = config.PositionPointOnScene;
            newPoint.transform.SetParent(holder, false);

            list.Add(newPoint);
        }

        return list;
    }
}
