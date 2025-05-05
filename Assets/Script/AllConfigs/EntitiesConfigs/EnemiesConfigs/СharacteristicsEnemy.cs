using System;
using UnityEngine;

[Serializable]
public class CharacteristicsEnemy
{
    [field: SerializeField] public EnemyType EnemyType {  get; private set; }
    [field: SerializeField, Range(0.1f, 30f)] public float MoveSpeed { get; private set; }
}
