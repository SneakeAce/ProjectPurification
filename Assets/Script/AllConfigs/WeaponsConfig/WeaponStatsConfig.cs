using System;
using UnityEngine;

[Serializable]
public class WeaponStatsConfig
{
    [field: SerializeField] public FiringMode FiringMode { get; private set; }
    [field: SerializeField] public BulletType BulletTypeInWeapon { get; private set; }
    [field: SerializeField] public int BaseMagazineCapacity { get; private set;}
    [field: SerializeField] public float BaseDelayBeforeFiring { get; private set; }
    [field: SerializeField, Range(0f, 30f)] public float BaseShootingRange { get; private set; }
    [field: SerializeField, Range(0.001f, 5f)] public float BaseReloadingTime { get; private set; }

}
