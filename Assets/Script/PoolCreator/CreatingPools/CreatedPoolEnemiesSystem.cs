using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolEnemiesSystem : CreatedPoolSystem<EnemyCharacter, EnemyType>
{
    public CreatedPoolEnemiesSystem(CreatedPoolEnemyConfig config) : base(config)
    {
    }

    protected override void Initialization()
    {
        _poolDictionary = new Dictionary<EnemyType, ObjectPool<EnemyCharacter>>();

        StartingCreatePools();
    }

    protected override void StartingCreatePools()
    {
        if (_monoObjects.Count > 0)
        {
            foreach (EnemyCharacter enemyCharacter in _monoObjects)
            {
                if (_poolDictionary.ContainsKey(enemyCharacter.EnemyType))
                    continue;

                switch (enemyCharacter.EnemyType)
                {
                    case EnemyType.NormalZombie:
                        ObjectPool<EnemyCharacter> poolNormalZombie = CreatePool(enemyCharacter.EnemyType, enemyCharacter, enemyCharacter.MaxCountOnCurrentScene);
                        _poolDictionary.Add(enemyCharacter.EnemyType, poolNormalZombie);
                        break;

                    case EnemyType.BigZombie:
                        ObjectPool<EnemyCharacter> pooligZombie = CreatePool(enemyCharacter.EnemyType, enemyCharacter, enemyCharacter.MaxCountOnCurrentScene);
                        _poolDictionary.Add(enemyCharacter.EnemyType, pooligZombie);

                        break;

                    case EnemyType.SpittingZombie:
                        ObjectPool<EnemyCharacter> poolSpittingZombie = CreatePool(enemyCharacter.EnemyType, enemyCharacter, enemyCharacter.MaxCountOnCurrentScene);
                        _poolDictionary.Add(enemyCharacter.EnemyType, poolSpittingZombie);

                        break;

                    default:
                        throw new ArgumentException("This Enemy type does not exist");
                }
            }
        }
    }

    protected override ObjectPool<EnemyCharacter> CreatePool(EnemyType enemyType, EnemyCharacter placeableObject, int maxPoolSize)
    {
        ObjectPool<EnemyCharacter> placeableObjectPool;

        GameObject newHolder = new GameObject(enemyType.ToString());
        newHolder.transform.SetParent(null);
        newHolder.transform.position = Vector3.zero;

        if (newHolder != null)
        {
            placeableObjectPool = new ObjectPool<EnemyCharacter>(placeableObject, maxPoolSize, newHolder.transform);

            return placeableObjectPool;
        }

        return null;
    }
}
