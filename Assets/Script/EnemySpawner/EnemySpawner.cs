using System.Collections;
using UnityEngine;

public abstract class EnemySpawner
{
    protected const int AttemptsForSearchNewSpawnPoint = 5;

    [SerializeField] protected PoolCreator _createdPools;
    [SerializeField] protected LayerMask _enemyLayer;
    [SerializeField] protected LayerMask _obstacleLayer;
    [SerializeField] protected LayerMask _groundLayer;
                     
    [SerializeField] protected float _radiusCheckingEnemyAround;
    [SerializeField] protected float _radiusCheckingObstacleAround;
    [SerializeField] protected float _timeBetweenSpawn;

    protected bool _isCanWork = false;

    protected SwitchBehavioralPattern _behavioralPattern;

    protected CreatedPoolEnemiesSystem PoolsEnemy => _createdPools.PoolEnemySystem;

    public void StartSpawnEnemy()
    {
        SpawnEnemy();
    }

    public abstract void SpawnEnemy();
    public abstract bool CheckEnemyAroundSpawnPoint(Vector3 spawnPointPosition);
    public abstract bool CheckObstacleAroundSpawnPoint(Vector3 spawnPointPosition);
    public abstract bool CheckGroundUnderSpawnPoint(Vector3 spawnPointPosition);
    public abstract void OnReturnEnemyToPool(EnemyHealth enemyHealth);

    public virtual void Initialization()
    {
        Debug.Log("poolsEnemy = " + PoolsEnemy);
        _isCanWork = true;
    }
}
