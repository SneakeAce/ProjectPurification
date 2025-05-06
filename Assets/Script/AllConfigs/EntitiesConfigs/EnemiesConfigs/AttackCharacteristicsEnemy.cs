using System;
using UnityEngine;

[Serializable]
public class AttackCharacteristicsEnemy
{
    [field: SerializeField] public AttackType AttackType { get; private set; }
    [field: SerializeField] public float BaseDamage { get; private set; }
    [field: SerializeField] public float BaseRadiusAttack { get; private set; }
    [field: SerializeField] public float BaseReloadingTime { get; private set; }
    [field: SerializeField] public float BaseSearchTargetRadius { get; private set; }
    [field: SerializeField] public LayerMask TargetLayer { get; private set; }
}
