using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EnemySpawnerConfig/LocalEnemySpawnerConfig", fileName = "LocalEnemySpawnerConfig")]
public class LocalEnemySpawnerConfig : EnemySpawnerConfig
{
    [field: SerializeField] public EnemyTypeInSpawner EnemyTypeInSpawner { get; private set; }
    [field: SerializeField] public float RadiusSpawn { get; private set; }
    [field: SerializeField] public int MaxEnemyOnSceneInCurrentLocalSpawner { get; private set; }
}                           
 