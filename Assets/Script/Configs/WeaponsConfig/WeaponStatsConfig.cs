using System;
using UnityEngine;

[Serializable]
public class WeaponStatsConfig
{
    [field: SerializeField] public WeaponsNames WeaponNames { get; private set; }
    [field: SerializeField] public WeaponAttackType WeaponAttackType { get; private set;}
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField, Range(0f, 100f)] public float RangeShooting { get; private set; }
    [field: SerializeField, Range(0f, 50f)] public float ReloadingTime { get; private set; }
}
