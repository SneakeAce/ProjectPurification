using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyFactory
{
    EnemyCharacter Create(Vector3 spawnPosition, EnemyType enemyTypeInSpawner, 
        float minRotationValue, float maxRotationValue);

}
