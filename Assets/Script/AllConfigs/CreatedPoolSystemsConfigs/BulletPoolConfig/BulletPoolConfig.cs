using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ObjectsPoolConfig/BulletPoolConfig", fileName = "BulletPoolConfig")]
public class BulletPoolConfig : ScriptableObject
{
    [field: SerializeField] public BulletType BulletType { get; private set; }
    [field: SerializeField] public Bullet Prefab { get; private set; }
    [field: SerializeField] public int MaxCountCurrentBulletOnScene { get; private set; }
}
