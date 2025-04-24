using System;
using UnityEngine;

public interface IFactory<TObject, TObjConfig, TEnum>
    where TObject : MonoBehaviour
    where TObjConfig : ScriptableObject
    where TEnum : Enum
{
    TObject Create(Vector3 spawnPosition, TEnum objectType,
    Quaternion rotation);

    TObjConfig GetObjectConfig(TEnum type);
}
