using System;
using UnityEngine;

[Serializable]
public class EntityHealthCharacteristics
{
    [field: SerializeField] public ArmorType ArmorType { get; private set; }
    [field: SerializeField, Range(0, 100)] public float ArmorValue { get; private set; }
    [field: SerializeField] public float BaseHealthValue { get; private set; }
}
