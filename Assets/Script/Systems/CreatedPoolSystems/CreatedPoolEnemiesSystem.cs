using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolEnemiesSystem : CreatedPoolSystem<EnemyCharacter, EnemyType>
{
    private CreatedPoolEnemyConfig _config;

    public CreatedPoolEnemiesSystem(CreatedPoolEnemyConfig config) : base(config)
    {
        _config = config;

        Initialization();
    }

    protected override void Initialization()
    {
        foreach(var config in _config.PoolEnemyConfigs)
        {
            Debug.Log("CreatedPoolEnemiesSystem / Init / config EnemyType = " + config.EnemyType + " / config name = " + config.name);
        }

        _poolDictionary = new Dictionary<EnemyType, ObjectPool<EnemyCharacter>>();

        StartingCreatePools();
    }

    protected override void StartingCreatePools()
    {
        Debug.Log("_Config == " + _config);

        if (_config.PoolEnemyConfigs.Count > 0)
        {
            foreach (var config in _config.PoolEnemyConfigs)
            {
                if (_poolDictionary.ContainsKey(config.EnemyType))
                    continue;

                Debug.Log("Config.EnemyType == " + config.EnemyType);

                ObjectPool<EnemyCharacter> pool = CreatePool(config.EnemyType, config.EnemyPrefab, config.MaxCountEnemyOnScene);
                _poolDictionary.Add(config.EnemyType, pool);
            }
        }
    }

    protected override ObjectPool<EnemyCharacter> CreatePool(EnemyType enemyType, EnemyCharacter enemyObject, int maxPoolSize)
    {        
        ObjectPool<EnemyCharacter> enemyObjectsPool;

        GameObject newHolder = new GameObject(enemyType.ToString());
        newHolder.transform.SetParent(null);
        newHolder.transform.position = Vector3.zero;

        if (newHolder != null)
        {
            enemyObjectsPool = new ObjectPool<EnemyCharacter>(enemyObject, maxPoolSize, newHolder.transform);

            return enemyObjectsPool;
        }

        return null;
    }
}
