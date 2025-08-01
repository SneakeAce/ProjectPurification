using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ObjectsPoolConfig/EnemyPoolConfig", fileName = "EnemyPoolConfig")]
public class EnemyPoolConfig : ScriptableObject
{
    [field: SerializeField] public EnemyType EnemyType { get; private set; }
    [field: SerializeField] public EnemyCharacter EnemyPrefab { get; private set; }
    [field: SerializeField] public int MaxCountCurrentEnemyOnScene { get; private set; }
}
