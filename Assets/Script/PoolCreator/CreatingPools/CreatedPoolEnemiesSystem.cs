using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolEnemiesSystem
{
    private List<EnemyCharacter> _enemyObjects;

    private Dictionary<EnemyType, ObjectPool<EnemyCharacter>> _poolDictionary;

    public CreatedPoolEnemiesSystem(List<EnemyCharacter> enemyObjects)
    {
        _enemyObjects = new List<EnemyCharacter>(enemyObjects);
    }

    public Dictionary<EnemyType, ObjectPool<EnemyCharacter>> PoolDictionary => _poolDictionary;

    public void Initialization()
    {
        _poolDictionary = new Dictionary<EnemyType, ObjectPool<EnemyCharacter>>();

        StartingCreatePools();
    }

    private ObjectPool<EnemyCharacter> CreatePool(EnemyType enemyType, int maxPoolSize, EnemyCharacter placeableObject)
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

    private void StartingCreatePools()
    {
        if (_enemyObjects.Count > 0)
        {
            foreach (EnemyCharacter enemyCharacter in _enemyObjects)
            {
                if (_poolDictionary.ContainsKey(enemyCharacter.EnemyType))
                    continue;

                switch (enemyCharacter.EnemyType)
                {
                    case EnemyType.NormalZombie:
                        ObjectPool<EnemyCharacter> poolNormalZombie = CreatePool(enemyCharacter.EnemyType, enemyCharacter.MaxCountOnCurrentScene, enemyCharacter);
                        _poolDictionary.Add(enemyCharacter.EnemyType, poolNormalZombie);
                        break;

                    case EnemyType.BigZombie:
                        ObjectPool<EnemyCharacter> pooligZombie = CreatePool(enemyCharacter.EnemyType, enemyCharacter.MaxCountOnCurrentScene, enemyCharacter);
                        _poolDictionary.Add(enemyCharacter.EnemyType, pooligZombie);

                        break;

                    case EnemyType.SpittingZombie:
                        ObjectPool<EnemyCharacter> poolSpittingZombie = CreatePool(enemyCharacter.EnemyType, enemyCharacter.MaxCountOnCurrentScene, enemyCharacter);
                        _poolDictionary.Add(enemyCharacter.EnemyType, poolSpittingZombie);

                        break;

                    default:
                        throw new ArgumentException("This barrier type does not exist");
                }
            }
        }
    }
}
