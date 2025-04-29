using UnityEngine;

[CreateAssetMenu(menuName = "Configs/SearchTargetSystemConfig", fileName = "SearchTargetSystemConfig")]
public abstract class SearchTargetSystemConfig : ScriptableObject
{
    [field: SerializeField] public float RadiusSearching { get; private set; }
    [field: SerializeField] public LayerMask TargetLayerMask { get; private set; }
}                                   
