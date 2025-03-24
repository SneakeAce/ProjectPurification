using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacementSystemConfig : ScriptableObject
{
    [field: SerializeField] public List<GameObject> PhantomObjectsPrefab { get; private set; }
    [field: SerializeField] public LayerMask ObstaclesLayer { get; private set; }
    [field: SerializeField] public Color ColorBeingPlacedObject { get; private set; }
    [field: SerializeField] public Color ColorNotBeingPlacedObject { get; private set; }
    [field: SerializeField] public float RadiusPlacing { get; private set; }
    [field: SerializeField] public float RotationSpeedObject { get; private set; }
    [field: SerializeField] public string ModeNameInPlayerInput { get; private set; }
}
