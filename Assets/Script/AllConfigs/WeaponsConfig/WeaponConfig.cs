using UnityEngine;

[CreateAssetMenu(menuName = "Configs/WeaponConfig", fileName = "WeaponConfig")]
public class WeaponConfig : ScriptableObject, IConfigWithType<WeaponType>
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public WeaponType WeaponType { get; private set; }
    [field: SerializeField] public WeaponStatsConfig WeaponStatsConfig { get; private set; }

    public WeaponType ConfigType => WeaponType;
}
