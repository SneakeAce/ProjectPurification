using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/SpawnPointForSpawner/SpawnPointsForSpawnerConfig", fileName = "SpawnPointsForSpawnerConfig")]
public class SpawnPointForSpawnerConfigs : ScriptableObject
{
    [field: SerializeField] public List<SpawnPointForSpawnerConfig> SpawnPointConfig { get; private set; }
}
