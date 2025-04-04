using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class LocalEnemySpawner : EnemySpawner
{
    private const float MinRotationValue = 0f;
    private const float MaxRotationValue = 360f;
    private const int AdditionalValue = 1;
    private const int ReducingValue = 1;

    private float _radiusSpawn;

    private int _maxEnemyOnScene;
    private int _currentEnemyOnScene;

    private LocalEnemySpawnerConfig _config;

    [Inject]
    private void Construct(LocalEnemySpawnerConfig config)
    {
        _config = config;

        _allowedEnemyTypeInSpawner = config.AllowedEnemyType;
        _radiusSpawn = config.RadiusSpawn;
        _maxEnemyOnScene = config.MaxEnemyOnSceneInCurrentLocalSpawner;
    }

    public override void Initialization()
    {
        _enemyLayer = _config.EnemyLayer;
        _groundLayer = _config.GroundLayer;
        _obstacleLayer = _config.ObstacleLayer;

        _radiusCheckingEnemyAround = _config.RadiusCheckingEnemyAround;
        _radiusCheckingObstacleAround = _config.RadiusCheckingObstacleAround;
        _timeBetweenSpawn = _config.TimeBetweenSpawn;

        base.Initialization();
    }

    public override void SpawnEnemy()
    {
        if (_currentEnemyOnScene >= _maxEnemyOnScene)
            return;

        EnemyCharacter enemy = GetEnemy();

        if (enemy == null)
            return;

        BehavioralPatternSwitcher behavioralPattern = enemy.GetComponentInChildren<BehavioralPatternSwitcher>();
        behavioralPattern.SetBehavioralPattern(enemy);
    }

    public override IEnumerator SpawnEnemyCoroutine()
    {
        while (_isCanWork)
        {
            yield return new WaitForSeconds(_timeBetweenSpawn);

            if (_currentEnemyOnScene < _maxEnemyOnScene)
                SpawnEnemy();
            else
                _isCanWork = false;

            yield return null;
        }
    }

    private Vector3 GetSpawnPoint()
    {
        for (int attempt = 0; attempt < AttemptsForSearchNewSpawnPoint; attempt++)
        {
            Vector3 newPositionEnemy = transform.position + (Random.insideUnitSphere * _radiusSpawn);
            newPositionEnemy.y = 0;

            if (CheckEnemyAroundSpawnPoint(newPositionEnemy) && CheckGroundUnderSpawnPoint(newPositionEnemy) && CheckObstacleAroundSpawnPoint(newPositionEnemy))
            {
                return newPositionEnemy;
            }
        }

        return Vector3.zero;
    }

    public override bool CheckEnemyAroundSpawnPoint(Vector3 spawnPointPosition)
    {
        Collider[] enemyInRadius = Physics.OverlapSphere(spawnPointPosition, _radiusCheckingEnemyAround, _enemyLayer);

        if (enemyInRadius.Length > 0)
            return false;

        return true;
    }

    public override bool CheckGroundUnderSpawnPoint(Vector3 spawnPointPosition)
    {
        Collider[] groundUnderEnemy = Physics.OverlapSphere(spawnPointPosition, _radiusCheckingObstacleAround, _groundLayer);

        if (groundUnderEnemy.Length > 0)
            return true;

        return false;
    }

    public override bool CheckObstacleAroundSpawnPoint(Vector3 spawnPointPosition)
    {
        Collider[] obstacleInRadius = Physics.OverlapSphere(spawnPointPosition, _radiusCheckingObstacleAround, _obstacleLayer);

        if (obstacleInRadius.Length > 0)
            return false;

        return true;
    }

    public override void OnReturnEnemyToPool(EnemyHealth enemyHealth)
    {
        EnemyCharacter enemy = enemyHealth.Enemy;

        _currentEnemyOnScene = _currentEnemyOnScene - ReducingValue;

        enemy.ReturnToPool(enemyHealth);

        enemyHealth.UnitDead -= OnReturnEnemyToPool;
    }

    private EnemyCharacter GetEnemy()
    {
        EnemyType currentEnemyType = GetEnemyType();

        if (currentEnemyType == EnemyType.None)
            return null;

        Vector3 newPosition = GetSpawnPoint();

        if (newPosition == Vector3.zero)
            return null;

        EnemyCharacter enemy = _enemyFactory.Create(newPosition, currentEnemyType, MinRotationValue, MaxRotationValue);

        if (enemy == null)
            return null;

        _currentEnemyOnScene += AdditionalValue;

        enemy.EnemyHealth.UnitDead += OnReturnEnemyToPool;

        return enemy;
    }

    private EnemyType GetEnemyType()
    {
        List<EnemyType> randomsEnemyTypes = GetRandomsEnemyTypes();

        if (randomsEnemyTypes.Count <= 0)
            return EnemyType.None;

        foreach(EnemyType enemyType in randomsEnemyTypes)
        {
            if (enemyType == _allowedEnemyTypeInSpawner)
            {
                return enemyType;
            }
        }

        return EnemyType.None;
    }

}
