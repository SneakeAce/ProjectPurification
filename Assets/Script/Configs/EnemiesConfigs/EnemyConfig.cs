using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EnemyConfig", fileName = "EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public SpecificationsEnemy SpecificationsEnemy { get; private set; }
}
