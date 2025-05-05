using System;
using UnityEngine;

[Serializable]
public class AttackCharacteristicsEnemy
{
    [field: SerializeField] public AttackType AttackType { get; private set; }
    [field: SerializeField] public float BaseDamage { get; private set; }
    [field: SerializeField] public float RadiusAttack { get; private set; }
    [field: SerializeField] public float ReloadingTime { get; private set; }
    [field: SerializeField] public LayerMask TargetLayer { get; private set; }
}
