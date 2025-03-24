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
    private const float MinRotationValue = 0f;
    private const float MaxRotationValue = 360f;

    private List<SpawnPoint> _spawnPoints;
    private List<EnemyType> _enemyTypes = new List<EnemyType>();

    private ObjectPool<EnemyCharacter> _selectedEnemyPool;
    private GlobalEnemySpawnerConfig _config;

    [Inject]
    private void Construct(GlobalEnemySpawnerConfig config, ISpawnPointFactory factory)
    {
        _config = config;

        CreateSpawnPoint(factory);

        GetEnemyTypeCount();
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

        if (enemy == null)
            return;

        SwitchBehavioralPattern behavioralPattern = enemy.GetComponentInChildren<SwitchBehavioralPattern>();
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

    private void CreateSpawnPoint(ISpawnPointFactory factory)
    {
        Transform holder = this.transform.Find(SpawnPointHolder);

        _spawnPoints = factory.Create(holder);
    }

    private Vector3 GetSpawnPoint(SpawnPoint selectedSpawnPoint)
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

        Debug.LogWarning($"Не удалось найти точку для спауна у {selectedSpawnPoint.name}, возвращаю стандартную.");
        return selectedSpawnPoint.transform.position;
    }

    private void GetEnemyTypeCount()
    {
        _enemyTypes.Clear();

        _enemyTypes = Enum.GetValues(typeof(EnemyType)).Cast<EnemyType>().ToList();
    }

    private EnemyCharacter GetEnemy()
    {
        _selectedEnemyPool = null;

        if (_enemyTypes.Count == 0)
            return null; 

        int randomNumber = Random.Range(MinRandomRangeValue, _enemyTypes.Count);
        int additionalValue = 1;

        if (Enum.IsDefined(typeof(EnemyType), randomNumber))
        {
            EnemyType enemyTypeSelected = (EnemyType)randomNumber;

            if (_enemiesPools.PoolDictionary.TryGetValue(enemyTypeSelected, out ObjectPool<EnemyCharacter> poolSelected))
            {
                _selectedEnemyPool = poolSelected;
            }
            else
            {
                Debug.LogWarning($"GetEnemy: Пул для {enemyTypeSelected} не найден!");
                return null;
            }
            
            foreach (SpawnPoint spawnPoint in _spawnPoints)
            {
                if (spawnPoint.EnemyTypeInSpawner == enemyTypeSelected && spawnPoint.CurrentEnemyOnScene < spawnPoint.MaxEnemyOnScene)
                {
                    Vector3 newPosition = GetSpawnPoint(spawnPoint);

                    if (newPosition == Vector3.zero)
                        continue;

                    EnemyCharacter enemy = _selectedEnemyPool.GetPoolObject();

                    if (enemy == null)
                    {
                        Debug.LogWarning($"GetEnemy: Не удалось получить врага из пула {_selectedEnemyPool}!");
                        return null;
                    }

                    enemy.Initialize();
                    enemy.SetPool(_selectedEnemyPool);

                    enemy.transform.position = newPosition;
                    enemy.transform.rotation = Quaternion.Euler(MinRotationValue, Random.Range(MinRotationValue, MaxRotationValue), MinRotationValue);

                    spawnPoint.CurrentEnemyOnScene = spawnPoint.CurrentEnemyOnScene + additionalValue;

                    enemy.EnemyHealth.UnitDead += OnReturnEnemyToPool;

                    return enemy;
                }
                else
                {
                    continue;
                }
            }

        }

        Debug.LogWarning("GetEnemy: Не найдено подходящее место для спауна.");
        return null;
    }


    public override void OnReturnEnemyToPool(EnemyHealth enemyHealth)
    {
        int decreasingValue = 1;

        EnemyCharacter enemy = enemyHealth.Enemy;

        foreach (SpawnPoint point in _spawnPoints)
        {
            if (point.EnemyTypeInSpawner == enemy.EnemyType)
            {
                point.CurrentEnemyOnScene = point.CurrentEnemyOnScene - decreasingValue;
                break;
            }
        }

        enemy.ReturnToPool(enemyHealth);

        enemyHealth.UnitDead -= OnReturnEnemyToPool;
    }
}
