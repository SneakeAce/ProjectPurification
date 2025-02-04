using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolEnemySystem : MonoBehaviour
{
    [SerializeField] private List<EnemyCharacter> _enemyObjects;

    [SerializeField] private int _maxPoolSizeForNormalZombie;
    [SerializeField] private int _maxPoolSizeForBigZombie;
    [SerializeField] private int _maxPoolSizeForSpittingZombie;

    private Dictionary<EnemyType, ObjectPool<EnemyCharacter>> _poolDictionary;

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
                        ObjectPool<EnemyCharacter> poolWoodBarrier = CreatePool(enemyCharacter.EnemyType, _maxPoolSizeForNormalZombie, enemyCharacter);
                        _poolDictionary.Add(enemyCharacter.EnemyType, poolWoodBarrier);
                        break;

                    case EnemyType.BigZombie:
                        ObjectPool<EnemyCharacter> poolMetallBarrier = CreatePool(enemyCharacter.EnemyType, _maxPoolSizeForBigZombie, enemyCharacter);
                        _poolDictionary.Add(enemyCharacter.EnemyType, poolMetallBarrier);

                        break;

                    case EnemyType.SpittingZombie:
                        ObjectPool<EnemyCharacter> poolConcreteBarrier = CreatePool(enemyCharacter.EnemyType, _maxPoolSizeForSpittingZombie, enemyCharacter);
                        _poolDictionary.Add(enemyCharacter.EnemyType, poolConcreteBarrier);

                        break;

                    default:
                        throw new ArgumentException("This barrier type does not exist");
                }
            }
        }
    }
}
