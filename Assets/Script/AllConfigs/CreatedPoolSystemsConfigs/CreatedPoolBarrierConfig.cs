using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/CreatedPoolBarrierConfig", fileName = "CreatedPoolBarrierConfig")]
public class CreatedPoolBarrierConfig : CreatedPoolSystemConfig<PlaceableObject>
{
    [field: SerializeField] public List<BarrierPoolConfig> PoolBarrierConfigs { get; private set; }
}
