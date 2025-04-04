using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolBarriersSystem : CreatedPoolSystem<PlaceableObject, BarriersType>
{
    public CreatedPoolBarriersSystem(CreatedPoolBarrirerConfig config) : base(config)
    {
        Initialization();
    }

    protected override void Initialization()
    {
        _poolDictionary = new Dictionary<BarriersType, ObjectPool<PlaceableObject>>();

        StartingCreatePools();
    }

    protected override void StartingCreatePools()
    {
        if (_monoObjects.Count > 0)
        {
            foreach (PlaceableObject placeableObject in _monoObjects)
            {
                if (_poolDictionary.ContainsKey(placeableObject.BarrierType))
                    continue;

                switch (placeableObject.BarrierType)
                {
                    case BarriersType.WoodBarrier:
                        ObjectPool<PlaceableObject> poolWoodBarrier = CreatePool(placeableObject.BarrierType, placeableObject, placeableObject.MaxCountOnCurrentScene);
                        _poolDictionary.Add(placeableObject.BarrierType, poolWoodBarrier);
                        break;

                    case BarriersType.MetallBarrier:
                        ObjectPool<PlaceableObject> poolMetallBarrier = CreatePool(placeableObject.BarrierType, placeableObject, placeableObject.MaxCountOnCurrentScene);
                        _poolDictionary.Add(placeableObject.BarrierType, poolMetallBarrier);

                        break;

                    case BarriersType.ConcreteBarrier:
                        ObjectPool<PlaceableObject> poolConcreteBarrier = CreatePool(placeableObject.BarrierType, placeableObject, placeableObject.MaxCountOnCurrentScene);
                        _poolDictionary.Add(placeableObject.BarrierType, poolConcreteBarrier);

                        break;

                    default:
                        throw new ArgumentException("This barrier type does not exist");
                }
            }
        }
    }

    protected override ObjectPool<PlaceableObject> CreatePool(BarriersType barrierType, PlaceableObject placeableObject, int maxPoolSize)
    {
        ObjectPool<PlaceableObject> placeableObjectPool;

        GameObject newHolder = new GameObject(barrierType.ToString());
        newHolder.transform.SetParent(null);
        newHolder.transform.position = Vector3.zero;

        if (newHolder != null)
        {
            placeableObjectPool = new ObjectPool<PlaceableObject>(placeableObject, maxPoolSize, newHolder.transform);

            return placeableObjectPool;
        }

        return null;
    }
}
