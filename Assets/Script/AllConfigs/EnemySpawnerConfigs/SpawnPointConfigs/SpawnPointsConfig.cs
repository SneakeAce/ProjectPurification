using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/SpawnPoint/SpawnPointsConfig", fileName = "SpawnPointsConfig")]
public class SpawnPointsConfig : ScriptableObject
{
    [field: SerializeField] public List<SpawnPointConfig> SpawnPointConfig { get; private set; }
}
