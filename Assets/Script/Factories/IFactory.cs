using System;
using UnityEngine;

public interface IFactory<TObject, TEnum>
    where TObject : MonoBehaviour
    where TEnum : Enum
{
    TObject Create(Vector3 spawnPosition, TEnum objectType,
    Quaternion rotation);
}
