using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/CreatedPoolConfigs/CreatedPoolBarrierConfig", fileName = "CreatedPoolBarrierConfig")]
public class CreatedPoolBarrierConfig : CreatedPoolSystemConfig<Barrier>
{
    [field: SerializeField] public List<BarrierPoolConfig> PoolBarrierConfigs { get; private set; }
}
