using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/CreatedPoolBulletConfig", fileName = "CreatedPoolBulletConfig")]
public class CreatedPoolBulletConfig : CreatedPoolSystemConfig<Bullet>
{
    [field: SerializeField] public List<BulletPoolConfig> PoolBulletConfigs { get; private set; }
}
