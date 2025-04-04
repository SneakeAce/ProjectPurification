using UnityEngine;
using Zenject;

public class EnemyFactory : IEnemyFactory
{
    private DiContainer _container;

    private CreatedPoolEnemiesSystem _enemiesPools;

    public EnemyFactory(DiContainer container, CreatedPoolEnemiesSystem enemiesPools)
    {
        _container = container;

        _enemiesPools = enemiesPools;
    }

    public EnemyCharacter Create(Vector3 spawnPosition, EnemyType enemyTypeInSpawner, 
        float minRotationValue, float maxRotationValue)
    {
        Attack enemyAttack;
        BehavioralPatternSwitcher patternSwitcher;

        ObjectPool<EnemyCharacter> enemyPool = GetPool(enemyTypeInSpawner);

        if (enemyPool == null)
            return null;

        EnemyCharacter enemy = enemyPool.GetPoolObject();

        if (enemy == null)
            return null;

        EnemyComponents(enemy, out enemyAttack, out patternSwitcher);

        _container.Inject(enemy);
        _container.Inject(patternSwitcher);

        enemy.SetAttackComponent(enemyAttack);

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
            return poolSelected;
        
        return null;
    }

    private void EnemyComponents(EnemyCharacter enemy, out Attack enemyAttack, out BehavioralPatternSwitcher patternSwitcher)
    {
        enemyAttack = enemy.GetComponentInChildren<Attack>();
        patternSwitcher = enemy.GetComponentInChildren<BehavioralPatternSwitcher>();
    }
}
