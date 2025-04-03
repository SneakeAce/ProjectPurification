using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

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

    private EnemyType _allowedEnemyTypeInSpawner;

    protected IEnemyFactory _enemyFactory;

    protected BehavioralPatternSwitcher _behavioralPattern;

    protected Coroutine _spawnEnemyCoroutine;

    [Inject]
    private void Construct(IEnemyFactory enemyFactory)
    {
        _enemyFactory = enemyFactory;
    }

    public abstract void SpawnEnemy();
    public abstract bool CheckEnemyAroundSpawnPoint(Vector3 spawnPointPosition);
    public abstract bool CheckObstacleAroundSpawnPoint(Vector3 spawnPointPosition);
    public abstract bool CheckGroundUnderSpawnPoint(Vector3 spawnPointPosition);
    public abstract void OnReturnEnemyToPool(EnemyHealth enemyHealth);
    public abstract IEnumerator SpawnEnemyCoroutine();

    private void Start()
    {
        //  —делать запуск спавнера из нормального места
        Initialization();
    }

    public virtual void Initialization()
    {
        _isCanWork = true;
        StartSpawnEnemy();
    }

    protected void StartSpawnEnemy()
    {
        _spawnEnemyCoroutine = StartCoroutine(SpawnEnemyCoroutine());
    }

    protected List<EnemyType> GetRandomsEnemyTypes()
    {
        List<EnemyType> availableEnemies = Enum.GetValues(typeof(EnemyType))
            .Cast<EnemyType>()
            .Where(type => type != EnemyType.None && (_allowedEnemyTypeInSpawner & type) != 0)
            .ToList();

        if (availableEnemies.Count <= 0)
            return null;

        return availableEnemies;
    }
}
