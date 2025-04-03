using UnityEngine;
using Zenject;

public class EnemyFactory : IEnemyFactory
{
    private DiContainer _container;

    private CreatedPoolEnemiesSystem _enemiesPools;

    private ObjectPool<EnemyCharacter> _enemyPool;

    public EnemyFactory(DiContainer container, CreatedPoolEnemiesSystem enemiesPools)
    {
        _container = container;

        _enemiesPools = enemiesPools;
    }

    public EnemyCharacter Create(Vector3 spawnPosition, EnemyType enemyTypeInSpawner, 
        float minRotationValue, float maxRotationValue)
    {
        ObjectPool<EnemyCharacter> enemyPool = GetPool(enemyTypeInSpawner);

        if (enemyPool == null)
            return null;

        EnemyCharacter enemy = enemyPool.GetPoolObject();

        if (enemy == null)
            return null;

        _container.Inject(enemy);

        enemy.Initialize();
        enemy.SetPool(enemyPool);

        enemy.transform.position = spawnPosition;
        enemy.transform.rotation = Quaternion.Euler(minRotationValue, Random.Range(minRotationValue, maxRotationValue), minRotationValue);

        return enemy;
    }

    private ObjectPool<EnemyCharacter> GetPool(EnemyType enemyTypeInSpawner)
    {
        EnemyType enemyTypeSelected = enemyTypeInSpawner;

        if (_enemiesPools.PoolDictionary.TryGetValue(enemyTypeSelected, out ObjectPool<EnemyCharacter> poolSelected))
        {
            _enemyPool = poolSelected;
            return poolSelected;
        }

        return null;
    }
}
