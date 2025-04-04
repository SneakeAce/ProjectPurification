using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class GlobalEnemySpawner : EnemySpawner
{
    private const string SpawnPointHolder = "SpawnPointsHolder";

    private const int MinRandomRangeValue = 0;
    private const int AdditionalValue = 1;
    private const int ReducingValue = 1;

    private const float MinRotationValue = 0f;
    private const float MaxRotationValue = 360f;

    private List<SpawnPoint> _spawnPoints;

    private ObjectPool<EnemyCharacter> _selectedEnemyPool;
    private GlobalEnemySpawnerConfig _config;

    [Inject]
    private void Construct(GlobalEnemySpawnerConfig config, ISpawnPointFactory factory)
    {
        _config = config;

        CreateSpawnPoint(factory);
    }

    public override IEnumerator SpawnEnemyCoroutine()
    {
        while (_isCanWork)
        {
            yield return new WaitForSeconds(_timeBetweenSpawn);

            SpawnEnemy();

            yield return null;
        }
    }

    public override void Initialization()
    {
        _allowedEnemyTypeInSpawner = _config.AllowedEnemyType;
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
        EnemyCharacter enemy = GetEnemy();

        Debug.Log("Enemy in GlobaleSpawner in SpawnEnemy = " + enemy);

        if (enemy == null)
            return;

        BehavioralPatternSwitcher behavioralPattern = enemy.GetComponentInChildren<BehavioralPatternSwitcher>();
        behavioralPattern.SetBehavioralPattern(enemy);
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

        foreach (SpawnPoint point in _spawnPoints)
        {
            if ((point.EnemyTypeInSpawnPoint & enemy.EnemyType) != 0)
            {
                point.DecreaseCurrentEnemy(ReducingValue);
                break;
            }
        }

        enemy.ReturnToPool(enemyHealth);

        enemyHealth.UnitDead -= OnReturnEnemyToPool;
    }

    private void CreateSpawnPoint(ISpawnPointFactory factory)
    {
        Transform holder = this.transform.Find(SpawnPointHolder);

        _spawnPoints = factory.Create(holder);
    }

    private EnemyCharacter GetEnemy()
    {
        EnemyType currentEnemyType;
        List<EnemyType> enemyTypes = GetRandomsEnemyTypes();

        if (enemyTypes.Count <= 0)
            return null;

        SpawnPoint newSpawnPoint = GetSpawnPoint(enemyTypes, out currentEnemyType);

        Debug.Log("NewSpawnPoint in GlobalSpawner = " + newSpawnPoint);

        if (newSpawnPoint == null)
            return null;

        Vector3 newPosition = GetSpawnPosition(newSpawnPoint);

        Debug.Log("NewPosition in GlobalSpawner = " + newPosition);

        if (newPosition == Vector3.zero)
            return null;

        EnemyCharacter enemy = _enemyFactory.Create(newPosition, currentEnemyType, MinRotationValue, MaxRotationValue);

        Debug.Log("Enemy in GlobalSpawner = " + enemy);

        if (enemy == null)
            return null;

        newSpawnPoint.IncreaseCurrentEnemy(AdditionalValue);

        enemy.EnemyHealth.UnitDead += OnReturnEnemyToPool;

        return enemy;
    }

    private SpawnPoint GetSpawnPoint(List<EnemyType> enemyTypes, out EnemyType currentEnemyType)
    {
        currentEnemyType = EnemyType.None;

        if (_spawnPoints.Count <= 0)
            return null;

        List<SpawnPoint> validSpawnPoints = _spawnPoints
            .Where(spawnPoint => enemyTypes.Any(enemyType => (spawnPoint.EnemyTypeInSpawnPoint & enemyType) != 0)) // ���������� ���� ������ � ��������� � ������
            .ToList();

        if (validSpawnPoints.Count <= 0)
            return null;

        SpawnPoint currentSpawnPoint = validSpawnPoints[Random.Range(0, validSpawnPoints.Count)];

        currentEnemyType = enemyTypes.FirstOrDefault(enemyType => (currentSpawnPoint.EnemyTypeInSpawnPoint & enemyType) != 0);

        return currentSpawnPoint;
    }

    private Vector3 GetSpawnPosition(SpawnPoint selectedSpawnPoint)
    {
        if (_spawnPoints.Count == 0)
            return Vector3.zero;

        for (int attempt = 0; attempt < AttemptsForSearchNewSpawnPoint; attempt++)
        {
            Vector3 newPositionEnemy = selectedSpawnPoint.transform.position + (Random.insideUnitSphere * selectedSpawnPoint.RadiusSpawning);
            newPositionEnemy.y = 0;

            if (CheckEnemyAroundSpawnPoint(newPositionEnemy) && CheckGroundUnderSpawnPoint(newPositionEnemy) && CheckObstacleAroundSpawnPoint(newPositionEnemy)) 
            {
                return newPositionEnemy;
            }
        }

        Debug.LogWarning($"�� ������� ����� ����� ��� ������ � {selectedSpawnPoint.name}, ��������� ����������� �������.");
        return selectedSpawnPoint.transform.position;
    }
}
