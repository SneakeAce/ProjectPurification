using UnityEngine;

[CreateAssetMenu(menuName = "Configs/TurretConfig", fileName = "TurretConfig")]
public class TurretConfig : ScriptableObject, IConfigWithType<TurretType>
{
    [field: SerializeField] public CharacteristicsTurret MainCharacteristics { get; private set; }

    public TurretType ConfigType => MainCharacteristics.TurretType;
}
