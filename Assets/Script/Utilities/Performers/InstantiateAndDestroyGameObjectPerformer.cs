using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAndDestroyGameObjectPerformer : MonoBehaviour
{
    public GameObject CreateObject(GameObject prefab, Vector3 spawnPosition, Quaternion rotation)
    {
        if (prefab == null)
            throw new Exception("GameObject in Performer cannot be created");
        
        GameObject item = Instantiate(prefab, spawnPosition, rotation);

        return item;
    }

    public void DestroyObject(GameObject item)
    {
        if (item != null)
            Destroy(item);
    }
}
