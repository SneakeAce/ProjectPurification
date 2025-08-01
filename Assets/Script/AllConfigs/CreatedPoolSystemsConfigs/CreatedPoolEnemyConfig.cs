using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/CreatedPoolConfigs/CreatedPoolEnemyConfig", fileName = "CreatedPoolEnemyConfig")]
public class CreatedPoolEnemyConfig : CreatedPoolSystemConfig<EnemyCharacter>
{
    [field: SerializeField] public List<EnemyPoolConfig> PoolEnemyConfigs { get; private set; }
}
