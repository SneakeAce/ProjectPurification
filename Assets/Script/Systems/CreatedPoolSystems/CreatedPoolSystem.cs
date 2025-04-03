using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatedPoolSystem<T, TEnum> 
    where T : MonoBehaviour 
    where TEnum : Enum
{
    protected List<T> _monoObjects;

    protected Dictionary<TEnum, ObjectPool<T>> _poolDictionary;

    public CreatedPoolSystem(CreatedPoolSystemConfig<T> config)
    {
        _monoObjects = new List<T>(config.Objects);

        Initialization();
    }

    public Dictionary<TEnum, ObjectPool<T>> PoolDictionary => _poolDictionary;

    protected abstract void Initialization();

    protected abstract void StartingCreatePools();

    protected abstract ObjectPool<T> CreatePool(TEnum barrierType, T monoObject, int maxPoolSize);
}
