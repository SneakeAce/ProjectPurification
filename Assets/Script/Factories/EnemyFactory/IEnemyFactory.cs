using UnityEngine;

public interface IEnemyFactory
{
    EnemyCharacter Create(Vector3 spawnPosition, EnemyType enemyTypeInSpawner, 
        Quaternion rotation);

}
