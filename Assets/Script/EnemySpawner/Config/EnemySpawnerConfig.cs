using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EnemySpawnerConfig")]
public class EnemySpawnerConfig : ScriptableObject
{
    [field: SerializeField] public Vector3 PositionSpawner { get; private set; }
    [field: SerializeField] public LayerMask EnemyLayer { get; private set; }
    [field: SerializeField] public LayerMask ObstacleLayer { get; private set; }
    [field: SerializeField] public LayerMask GroundLayer { get; private set; }
    [field: SerializeField] public float RadiusCheckingEnemyAround { get; private set; }
    [field: SerializeField] public float RadiusCheckingObstacleAround { get; private set; }
    [field: SerializeField] public float TimeBetweenSpawn { get; private set; }
}
