using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> _pool;
    private T _prefab;
    private Transform _container;
    private bool _isExpandPool;

    public ObjectPool(T prefab, int initialSize, Transform container = null, bool isExpandPool = false) 
    { 
        _prefab = prefab;
        _container = container;
        _isExpandPool = isExpandPool;

        CreatePool(initialSize);
    }

    public T GetPoolObject()
    {
        if (PoolObjectIsFree(out T poolObject))
            return poolObject;

        if (_isExpandPool) 
            return CreatePoolObject(_isExpandPool);

        throw new Exception("Pool is not expand. We used all active objects");
    }

    public void ReturnPoolObject(T poolObject)
    {
        poolObject.gameObject.SetActive(false);
        _pool.Enqueue(poolObject);
    }

    private void CreatePool(int sizePool)
    {
        _pool = new Queue<T>();

        for (int i = 0; i < sizePool; i++)
            CreatePoolObject();
    }

    private T CreatePoolObject(bool isActiveByDefault = false)
    {
        T poolObject = Object.Instantiate(_prefab, _container);
        poolObject.gameObject.SetActive(isActiveByDefault);
        _pool.Enqueue(poolObject);
        return poolObject;
    }

    private bool PoolObjectIsFree(out T poolObject)
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            poolObject = _pool.Dequeue();

            if (poolObject.gameObject.activeInHierarchy == false)
            {
                poolObject.gameObject.SetActive(true);
                _pool.Enqueue(poolObject);
                return true;
            }

            _pool.Enqueue(poolObject);
        }

        poolObject = null;
        return false;
    }


}
