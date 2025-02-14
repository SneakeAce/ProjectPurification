using UnityEngine;

[CreateAssetMenu(menuName = "Configs/BarrierConfig", fileName = "BarrierConfig")]
public class BarrierConfig : ScriptableObject
{
    [field: SerializeField] public CharacteristicsBarrier ÑharacteristicsBarrier { get; private set; }
}
