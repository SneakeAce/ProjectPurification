using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/SpawnPointForSpawner/SpawnPointForSpawnerConfig", fileName = "SpawnPointForSpawnerConfig")]
public class SpawnPointForSpawnerConfig : ScriptableObject
{
    [field: SerializeField] public Vector3 PositionPointOnScene { get; private set; }
    [field: SerializeField] public GameObject SpawnPointPrefab { get; private set; }

    [field: SerializeField] public EnemyType EnemyTypeInSpawnPoint { get; private set; }
    [field: SerializeField] public float RadiusSpawning { get; private set; }
    [field: SerializeField] public int MaxEnemyOnScene { get; private set; }

}
