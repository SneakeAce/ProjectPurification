using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/TurretConfig", fileName = "TurretConfig")]
public class TurretConfig : ScriptableObject
{
    [field: SerializeField] public CharacteristicsTurret ÑharacteristicsTurret { get; private set; }
}
