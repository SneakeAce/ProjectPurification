using System;
using UnityEngine;

[Serializable]
public class UniquePlayerCharacteristics
{
    [field: SerializeField, Range(0, 15)] public float MoveSpeed { get; private set; }
    [field: SerializeField] public Character PlayerPrefab { get; private set; }
    [field: SerializeField] public LayerMask IncludeLayerForMovement { get; private set; }
    [field: SerializeField] public Vector3 SpawnCoordinate { get; private set; }
}
