using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ObjectsPoolConfig/BarrierPoolConfig", fileName = "BarrierPoolConfig")]
public class BarrierPoolConfig : ScriptableObject
{
    [field: SerializeField] public BarriersType BarrierType { get; private set; }
    [field: SerializeField] public Barrier Prefab { get; private set; }
    [field: SerializeField] public int MaxCountCurrentBarrierOnScene { get; private set; }
}
