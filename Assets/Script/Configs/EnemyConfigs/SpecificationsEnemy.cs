using System;
using UnityEngine;

[Serializable]
public class SpecificationsEnemy
{
    [field: SerializeField] public EnemyType EnemyType {  get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public int MaxCountOnCurrentScne { get; private set; }
}
