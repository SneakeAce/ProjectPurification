using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ObjectsPoolConfig/TurretPoolConfig", fileName = "TurretPoolConfig")]
public class TurretPoolConfig : ScriptableObject
{
    [field: SerializeField] public TurretType TurretType { get; private set; }
    [field: SerializeField] public Turret Prefab { get; private set; }
    [field: SerializeField] public int MaxCountCurrentTurretOnScene { get; private set; }
}
