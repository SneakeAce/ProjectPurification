using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EntitiesConfigs/PlayerConfig", fileName = "PlayerConfig")]
public class PlayerConfig : ScriptableObject, IEntityConfig
{
    [field: SerializeField] public UniquePlayerCharacteristics UniqueCharacterisitcs { get; private set; }
    [field: SerializeField] public EntityHealthCharacteristics HealthCharacteristics { get; private set; }
}
