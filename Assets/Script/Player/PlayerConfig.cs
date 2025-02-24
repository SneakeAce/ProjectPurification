using UnityEngine;

[CreateAssetMenu(menuName = "Configs/PlayerConfig", fileName = "PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField, Range(0, 15)] public float Speed { get; private set; }
    [field: SerializeField, Range(0, 200)] public float MaxHealth { get; private set; }
    [field: SerializeField] public Character PlayerPrefab { get; private set; }
    [field: SerializeField] public LayerMask IncludeLayerForMovement { get; private set; }
    [field: SerializeField] public Vector3 SpawnCoordinate { get; private set; }
}
