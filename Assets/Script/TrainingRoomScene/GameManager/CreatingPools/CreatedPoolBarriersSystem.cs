using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolBarriersSystem
{
    private List<PlaceableObject> _placeableObjects;

    private Dictionary<BarriersType, ObjectPool<PlaceableObject>> _poolDictionary;

    public CreatedPoolBarriersSystem(IEnumerable<PlaceableObject> placeableObjects)
    {
        _placeableObjects = new List<PlaceableObject>(placeableObjects);
    }

    public Dictionary<BarriersType, ObjectPool<PlaceableObject>> PoolDictionary => _poolDictionary;
  
    public void Initialization()
    {
        _poolDictionary = new Dictionary<BarriersType, ObjectPool<PlaceableObject>>();

        StartingCreatePools();
    }

    private void StartingCreatePools()
    {
        if (_placeableObjects.Count > 0)
        {
            foreach (PlaceableObject placeableObject in _placeableObjects)
            {
                if (_poolDictionary.ContainsKey(placeableObject.BarrierType))
                    continue;

                switch (placeableObject.BarrierType)
                {
                    case BarriersType.WoodBarrier:
                        ObjectPool<PlaceableObject> poolWoodBarrier = CreatePool(placeableObject.BarrierType, placeableObject.MaxCountOnCurrentScene, placeableObject);
                        _poolDictionary.Add(placeableObject.BarrierType, poolWoodBarrier);
                        break;

                    case BarriersType.MetallBarrier:
                        ObjectPool<PlaceableObject> poolMetallBarrier = CreatePool(placeableObject.BarrierType, placeableObject.MaxCountOnCurrentScene, placeableObject);
                        _poolDictionary.Add(placeableObject.BarrierType, poolMetallBarrier);

                        break;

                    case BarriersType.ConcreteBarrier:
                        ObjectPool<PlaceableObject> poolConcreteBarrier = CreatePool(placeableObject.BarrierType, placeableObject.MaxCountOnCurrentScene, placeableObject);
                        _poolDictionary.Add(placeableObject.BarrierType, poolConcreteBarrier);

                        break;

                    default:
                        throw new ArgumentException("This barrier type does not exist");
                }
            }
        }
    }

    private ObjectPool<PlaceableObject> CreatePool(BarriersType barrierType, int maxPoolSize, PlaceableObject placeableObject)
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
