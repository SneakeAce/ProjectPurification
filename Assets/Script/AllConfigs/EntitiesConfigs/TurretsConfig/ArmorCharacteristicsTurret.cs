using System;
using UnityEngine;

[Serializable]
public class ArmorCharacteristicsTurret
{
    [field: SerializeField] public ArmorType ArmorType { get; private set; }
    [field: SerializeField] public float ArmorFactor { get; private set; }
}
