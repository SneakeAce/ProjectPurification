using System;
using UnityEngine;

[Serializable]
public class TurretDefenseAttributes
{
    [field: SerializeField] public ArmorType ArmorType { get; private set; }
    [field: SerializeField, Range(0, 100)] public float ArmorFactor { get; private set; }
    [field: SerializeField] public float BaseEndurance { get; private set; }

}
