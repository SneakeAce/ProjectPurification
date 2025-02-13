using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpecificationTurret
{
    [field: SerializeField] public TurretType TurretType { get; private set; }

    [field: SerializeField] public float FireRate { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }

    [field: SerializeField] public float MaxEndurance { get; private set; }

    [field: SerializeField] public int MaxCountOnScene { get; private set; }
}
