using System;
using UnityEngine;

[Serializable]
public class AttackCharacteristicsTurret
{
    [field: SerializeField] public AttackType AttackType { get; private set; }
    [field: SerializeField] public BulletType BulletType { get; private set; }
    [field: SerializeField] public float BaseDamage { get; private set; }
    [field: SerializeField] public float BaseDelayBeforeFiring { get; private set; }
    [field: SerializeField] public float BaseAttackRange { get; private set; }
    [field: SerializeField] public LayerMask TargetLayer { get; private set; }

}
