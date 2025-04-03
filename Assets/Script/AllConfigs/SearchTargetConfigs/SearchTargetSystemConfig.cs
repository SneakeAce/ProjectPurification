using UnityEngine;

[CreateAssetMenu(menuName = "Configs/SearchTargetSystemConfig", fileName = "SearchTargetSystemConfig")]
public class SearchTargetSystemConfig : ScriptableObject
{
    [field: SerializeField] public float MaxRadiusSearching { get; private set; }
    [field: SerializeField] public LayerMask TargetLayerMask { get; private set; }
}                                   
