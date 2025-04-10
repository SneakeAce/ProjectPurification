using UnityEngine;

[CreateAssetMenu(menuName = "Configs/BulletConfig", fileName = "BulletConfig")]
public class BulletConfig : ScriptableObject, IConfigWithType<BulletType>
{
    [field: SerializeField] public BulletType BulletType { get; private set; }
    [field: SerializeField] public AttackType AttackType { get; private set; }
    [field: SerializeField] public float BaseBulletDamage { get; private set; }
    [field: SerializeField] public float BaseBulletSpeed { get; private set; }
    [field: SerializeField] public LayerMask EnemyLayer { get; private set; }

    public BulletType ConfigType => BulletType;
}
