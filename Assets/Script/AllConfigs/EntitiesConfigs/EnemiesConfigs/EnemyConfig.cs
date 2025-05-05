using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EnemyConfig", fileName = "EnemyConfig")]
public class EnemyConfig : ScriptableObject, IEntityConfig, IConfigWithType<EnemyType>
{
    [field: SerializeField] public CharacteristicsEnemy CharacteristicsEnemy { get; private set; }
    [field: SerializeField] public AttackCharacteristicsEnemy AttackCharacteristics { get; private set; }
    [field: SerializeField] public EntityHealthCharacteristics HealthCharacteristics { get; private set; }

    public EnemyType ConfigType => CharacteristicsEnemy.EnemyType;
}
