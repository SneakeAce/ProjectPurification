using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EnemySpawnerConfig/GlobalEnemySpawnerConfig", fileName = "GlobalEnemySpawnerConfig")]
public class GlobalEnemySpawnerConfig : EnemySpawnerConfig
{
    [field: SerializeField] public List<SpawnPoint> SpawnPoints { get; private set; }
}
