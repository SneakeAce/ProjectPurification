using System;
using UnityEngine;

[Serializable]
public class AttackCharacteristicsBarrier
{
    [field: SerializeField] public AttackType AttackType { get; private set; }
    [field: SerializeField] public int BaseCountTargetForAttacking { get; private set; }
    [field: SerializeField] public float BaseDamage { get; private set; }
    [field: SerializeField] public float BaseDelayBetweenAttack { get; private set; }
    [field: SerializeField, Range(0.1f, 10f)] public float BaseRadiusAttack { get; private set; }
    [field: SerializeField] public LayerMask EnemyLayer { get; private set; }
}
