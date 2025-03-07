using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/BulletConfig", fileName = "BulletConfig")]
public class BulletConfig : ScriptableObject
{
    [field: SerializeField] public Bullet BulletPrefab { get; private set; }
    [field: SerializeField] public BulletType BulletType { get; private set; }
    [field: SerializeField] public int MaxCountOnScene { get; private set; }
    [field: SerializeField] public float BulletSpeed { get; private set; }
}
