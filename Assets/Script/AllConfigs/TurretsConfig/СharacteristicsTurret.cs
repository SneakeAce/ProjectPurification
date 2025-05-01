using System;
using UnityEngine;

[Serializable]
public class CharacteristicsTurret
{
    [field: SerializeField] public TurretType TurretType { get; private set; }
}
