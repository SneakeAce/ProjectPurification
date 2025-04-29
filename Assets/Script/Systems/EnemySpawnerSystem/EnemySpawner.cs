using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    protected EnemyType _allowedEnemyTypeInSpawner;

    protected IFactory<EnemyCharacter, EnemyConfig, EnemyType> _enemyFactory;

    protected BehavioralPatternSwitcher _behavioralPattern;

    protected Coroutine _spawnEnemyCoroutine;

    [Inject]
    private void Construct(IFactory<EnemyCharacter, EnemyConfig, EnemyType> enemyFactory)
    {
        _enemyFactory = enemyFactory;
    }

    public abstract void SpawnEnemy();
    public abstract bool CheckEnemyAroundSpawnPoint(Vector3 spawnPointPosition);
    public abstract bool CheckObstacleAroundSpawnPoint(Vector3 spawnPointPosition);
    public abstract bool CheckGroundUnderSpawnPoint(Vector3 spawnPointPosition);
    public abstract void OnReturnEnemyToPool(IEnemy enemy);
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
            return new List<EnemyType>();

        return availableEnemies;
    }
}
