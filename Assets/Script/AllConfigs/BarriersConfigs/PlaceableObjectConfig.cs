using UnityEngine;

[CreateAssetMenu(menuName = "Configs/BarrierConfig", fileName = "BarrierConfig")]
public class PlaceableObjectConfig : ScriptableObject, IConfigWithType<BarriersType>
{
    [field: SerializeField] public CharacteristicsBarrier Main—haracteristics { get; private set; }
    [field: SerializeField] public HealthCharacteristicsBarrier Health—haracteristics { get; private set; }
    [field: SerializeField] public AttackCharacteristicsBarrier AttackCharacteristics { get; private set; }

    public BarriersType ConfigType => Main—haracteristics.BarrierType;
}
