using System.Collections;
using UnityEngine;
using Zenject;

public abstract class EnemySpawner : MonoBehaviour
{
    protected const int AttemptsForSearchNewSpawnPoint = 5;

    protected LayerMask _enemyLayer;
    protected LayerMask _groundLayer;
    protected LayerMask _obstacleLayer;
    
    protected float _radiusCheckingEnemyAround;
    protected float _radiusCheckingObstacleAround;
    protected float _timeBetweenSpawn;

    protected bool _isCanWork = false;

    protected SwitchBehavioralPattern _behavioralPattern;

    protected CreatedPoolEnemiesSystem _enemiesPools;

    protected Coroutine _spawnEnemyCoroutine;

    [Inject]
    private void Construct(CreatedPoolEnemiesSystem enemiesPools)
    {
        _enemiesPools = enemiesPools;
    }

    public abstract void SpawnEnemy();
    public abstract bool CheckEnemyAroundSpawnPoint(Vector3 spawnPointPosition);
    public abstract bool CheckObstacleAroundSpawnPoint(Vector3 spawnPointPosition);
    public abstract bool CheckGroundUnderSpawnPoint(Vector3 spawnPointPosition);
    public abstract void OnReturnEnemyToPool(EnemyHealth enemyHealth);
    public abstract IEnumerator SpawnEnemyCoroutine();

    private void Start()
    {
        Initialization();
    }

    public virtual void Initialization()
    {
        _isCanWork = true;
        StartSpawnEnemy();
    }

    public void StartSpawnEnemy()
    {
        _spawnEnemyCoroutine = StartCoroutine(SpawnEnemyCoroutine());
    }
}
