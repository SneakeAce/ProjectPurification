using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolBarrierSystem : MonoBehaviour
{
    [SerializeField] private List<PlaceableObject> _placeableObjects;
    [SerializeField] private int _maxPoolSizeForWoodBarrier;    
    [SerializeField] private int _maxPoolSizeForMetallBarrier;
    [SerializeField] private int _maxPoolSizeForConcreteBarrier;

    private Dictionary<BarrierType, ObjectPool<PlaceableObject>> _poolDictionary;

    public Dictionary<BarrierType, ObjectPool<PlaceableObject>> PoolDictionary => _poolDictionary; 

    public void Initialization()
    {
        _poolDictionary = new Dictionary<BarrierType, ObjectPool<PlaceableObject>>();

        StartingCreatePools();
    }

    private ObjectPool<PlaceableObject> CreatePool(BarrierType barrierType, int maxPoolSize, PlaceableObject placeableObject)
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
                    case BarrierType.WoodBarrier:
                        ObjectPool<PlaceableObject> poolWoodBarrier = CreatePool(placeableObject.BarrierType, _maxPoolSizeForWoodBarrier, placeableObject);
                        _poolDictionary.Add(placeableObject.BarrierType, poolWoodBarrier);
                        break;

                    case BarrierType.MetallBarrier:
                        ObjectPool<PlaceableObject> poolMetallBarrier = CreatePool(placeableObject.BarrierType, _maxPoolSizeForMetallBarrier, placeableObject);
                        _poolDictionary.Add(placeableObject.BarrierType, poolMetallBarrier);

                        break;

                    case BarrierType.ConcreteBarrier:
                        ObjectPool<PlaceableObject> poolConcreteBarrier = CreatePool(placeableObject.BarrierType, _maxPoolSizeForConcreteBarrier, placeableObject);
                        _poolDictionary.Add(placeableObject.BarrierType, poolConcreteBarrier);

                        break;

                    default:
                        throw new ArgumentException("This barrier type does not exist");
                }
            }
        }
    }
}
