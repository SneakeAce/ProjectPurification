using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalEnemySpawner : EnemySpawner
{

    [SerializeField] protected EnemyTypeInSpawner _enemyTypeInSpawner;



    public override void SpawnEnemy()
    {
      
    }

    public override IEnumerator SpawningJob()
    {
        throw new System.NotImplementedException();
    }
    public override bool CheckEnemyAroundSpawnPoint(Vector3 spawnPointPosition)
    {
        throw new System.NotImplementedException();
    }

    public override bool CheckGroundUnderSpawnPoint(Vector3 spawnPointPosition)
    {
        throw new System.NotImplementedException();
    }

    public override bool CheckObstacleAroundSpawnPoint(Vector3 spawnPointPosition)
    {
        throw new System.NotImplementedException();
    }

    public Vector3 GetSpawnPoint()
    {
        throw new System.NotImplementedException();
    }
}
