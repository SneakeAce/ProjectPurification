using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/CreatedPoolConfigs/CreatedPoolTurretConfig", fileName = "CreatedPoolTurretConfig")]
public class CreatedPoolTurretConfig : CreatedPoolSystemConfig<Turret>
{
    [field: SerializeField] public List<TurretPoolConfig> PoolTurretsConfigs { get; private set; }
}
