using UnityEngine;
using Zenject;

public class EnemyFactory : IFactory<EnemyCharacter, EnemyType>
{
    private DiContainer _container;

    private CreatedPoolEnemiesSystem _enemiesPools;

    private ConfigsLibrariesHandler<EnemyConfig, EnemyType> _handlerEnemyConfigs;

    public EnemyFactory(DiContainer container, CreatedPoolEnemiesSystem enemiesPools, ConfigsLibrariesHandler<EnemyConfig, EnemyType> handlerEnemyConfigs)
    {
        _container = container;

        _enemiesPools = enemiesPools;

        _handlerEnemyConfigs = handlerEnemyConfigs;
    }

    public EnemyCharacter Create(Vector3 spawnPosition, EnemyType enemyTypeInSpawner, 
        Quaternion rotation)
    {
        Attack enemyAttack;
        BehavioralPatternSwitcher patternSwitcher;

        ObjectPool<EnemyCharacter> enemyPool = GetPool(enemyTypeInSpawner);

        if (enemyPool == null)
            return null;

        EnemyCharacter enemy = enemyPool.GetPoolObject();

        //Debug.Log("EnemyFactory / create / enemy = " + enemy);

        if (enemy == null)
            return null;

        EnemyConfig config = GetEnemyConfig(enemyTypeInSpawner);

        //Debug.Log("EnemyFactory / create / enemyCoonfig = " + config);

        GetEnemyComponents(enemy, out enemyAttack, out patternSwitcher);

        _container.Inject(enemy);
        _container.Inject(patternSwitcher);

        enemy.SetEnemyComponents(config, enemyAttack);

        enemy.Initialize();
        enemy.SetPool(enemyPool);

        enemy.transform.position = spawnPosition;
        enemy.transform.rotation = rotation;

        return enemy;
    }

    private ObjectPool<EnemyCharacter> GetPool(EnemyType enemyTypeInSpawner)
    {
        EnemyType enemyTypeSelected = enemyTypeInSpawner;

        if (_enemiesPools.PoolDictionary.TryGetValue(enemyTypeSelected, out ObjectPool<EnemyCharacter> poolSelected))
            return poolSelected;
        
        return null;
    }

    private void GetEnemyComponents(EnemyCharacter enemy, out Attack enemyAttack, out BehavioralPatternSwitcher patternSwitcher)
    {
        enemyAttack = enemy.GetComponentInChildren<Attack>();
        patternSwitcher = enemy.GetComponentInChildren<BehavioralPatternSwitcher>();
    }

    private EnemyConfig GetEnemyConfig(EnemyType type)
    {
        EnemyConfig config = _handlerEnemyConfigs.GetObjectConfig(type);

        if (config == null)
            return null;

        return config;
    }
}
