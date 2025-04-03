using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/SpawnPatrolPointsConfig", fileName = "SpawnPatrolPointsConfig")]
public class SpawnPatrolPointsConfig : ScriptableObject
{
    [field: SerializeField] public GameObject PointPrefab { get; private set; }
                            
    [field: SerializeField] public LayerMask AnothersPointLayer { get; private set; }
    [field: SerializeField] public LayerMask ObstacleLayer { get; private set; }

    [field: SerializeField, Range(1, 10)] public int MaxPatrolPoints { get; private set; }
    [field: SerializeField, Range(1, 100)] public float MaxRadiusSpawnPoint { get; private set; }

    [field: SerializeField, Range(0.1f, 15f)] public float RadiusCheckingAnotherNearestPoint { get; private set; }
    [field: SerializeField, Range(0.1f, 15f)] public float RadiusCheckingObstacle { get; private set; }
}
