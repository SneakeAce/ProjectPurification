using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/LibraryConfigs", fileName = "LibraryEnemyConfigs")]
public class LibraryConfigs<T> : ScriptableObject, ILibraryConfig
    where T : ScriptableObject
    
{
    [field: SerializeField] public List<T> Configs { get; private set; }
}
