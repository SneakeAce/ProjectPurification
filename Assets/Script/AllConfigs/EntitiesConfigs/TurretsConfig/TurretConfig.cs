using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EntitiesConfigs/TurretConfig", fileName = "TurretConfig")]
public class TurretConfig : ScriptableObject, IEntityConfig, IConfigWithType<TurretType>
{
    [field: SerializeField] public CharacteristicsTurret MainCharacteristics { get; private set; }
    [field: SerializeField] public AttackCharacteristicsTurret AttackCharacteristics { get; private set; }
    [field: SerializeField] public EntityHealthCharacteristics HealthCharacteristics { get; private set; }

    public TurretType ConfigType => MainCharacteristics.TurretType;
}
