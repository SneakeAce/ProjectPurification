using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EnemySpawnerConfig/GlobalEnemySpawnerConfig", fileName = "GlobalEnemySpawnerConfig")]
public class GlobalEnemySpawnerConfig : EnemySpawnerConfig
{
    [field: SerializeField] public EnemyType AllowedEnemyType { get; private set; }
    [field: SerializeField, Range(0, 300)] public int MaxEnemyOnScene { get; private set; }
}
