using System;
using UnityEngine;

[Serializable]
public class CharacteristicsTurret
{
    [field: SerializeField] public TurretType TurretType { get; private set; }
    [field: SerializeField] public float MaxEndurance { get; private set; }
}
