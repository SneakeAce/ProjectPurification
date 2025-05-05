using System;
using UnityEngine;

[Serializable]
public class AttackCharacteristicsBarrier
{
    [field: SerializeField] public AttackType AttackType { get; private set; }
    [field: SerializeField] public float BaseDamage { get; private set; }
    [field: SerializeField] public float BaseStartTimeBetweenAttack { get; private set; }
    [field: SerializeField] public LayerMask EnemyLayer { get; private set; }
}
