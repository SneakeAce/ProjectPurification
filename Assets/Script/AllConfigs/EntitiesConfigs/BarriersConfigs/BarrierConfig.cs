using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EntitiesConfigs/BarrierConfig", fileName = "BarrierConfig")]
public class BarrierConfig : ScriptableObject, IEntityConfig, IConfigWithType<BarriersType>
{
    [field: SerializeField] public CharacteristicsBarrier Main—haracteristics { get; private set; }
    [field: SerializeField] public AttackCharacteristicsBarrier AttackCharacteristics { get; private set; }
    [field: SerializeField] public EntityHealthCharacteristics HealthCharacteristics { get; private set; }

    public BarriersType ConfigType => Main—haracteristics.BarrierType;
}
