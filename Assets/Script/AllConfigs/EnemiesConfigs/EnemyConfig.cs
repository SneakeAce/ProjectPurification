using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EnemyConfig", fileName = "EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public CharacteristicsEnemy CharacteristicsEnemy { get; private set; }
    [field: SerializeField] public AttackCharacteristicsEnemy AttackCharacteristicsEnemy { get; private set; }
    [field: SerializeField] public HealthCharacteristicsEnemy HealthCharacteristicsEnemy { get; private set; }
}
