using UnityEngine;

[CreateAssetMenu(menuName = "Configs/WeaponConfig", fileName = "WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public WeaponStatsConfig WeaponStatsConfig { get; private set; }
}
