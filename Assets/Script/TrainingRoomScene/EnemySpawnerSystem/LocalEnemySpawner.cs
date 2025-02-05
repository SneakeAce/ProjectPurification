using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LocalEnemySpawner : EnemySpawner
{
    private const float MinRotationValue = 0f;
    private const float MaxRotationValue = 360f;

    [SerializeField] private EnemyTypeInSpawner _enemyTypeInSpawner;
    [SerializeField] private float _radiusSpawn;
    [SerializeField] private int _maxEnemyOnScene;

    private int _currentEnemyOnScene;

    private ObjectPool<EnemyCharacter> _selectedEnemyPool;

    public override void Initialization()
    {
        _selectedEnemyPool = GetPool();

        base.Initialization();
    }

    public override void SpawnEnemy()
    {
        Vector3 newPosition = GetSpawnPoint();

        if (newPosition == Vector3.zero)
            return;

        EnemyCharacter enemy = GetEnemy(newPosition);
        Debug.Log("enemy = " + enemy);
        if (enemy == null)
            return;

        SwitchBehavioralPattern behavioralPattern = enemy.GetComponentInChildren<SwitchBehavioralPattern>();
        behavioralPattern.SetBehavioralPattern(enemy);
    }

    public override IEnumerator SpawningJob()
    {
        while (_isCanWork)
        {
            yield return new WaitForSeconds(_timeBetweenSpawn);

            if (_currentEnemyOnScene < _maxEnemyOnScene)
            {
                SpawnEnemy();
            }
        }
    }

    public Vector3 GetSpawnPoint()
    {
        for (int attempt = 0; attempt < AttemptsForSearchNewSpawnPoint; attempt++)
        {
            Vector3 newPositionEnemy = transform.position + (Random.insideUnitSphere * _radiusSpawn);
            newPositionEnemy.y = 0;

            if (CheckEnemyAroundSpawnPoint(newPositionEnemy) && CheckGroundUnderSpawnPoint(newPositionEnemy) && CheckObstacleAroundSpawnPoint(newPositionEnemy))
            {
                Debug.Log("return newPositionenemy == " + newPositionEnemy);
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

    private ObjectPool<EnemyCharacter> GetPool()
    {
        EnemyType enemyTypeSelected = (EnemyType)_enemyTypeInSpawner;

        if (_poolsEnemy.PoolDictionary.TryGetValue(enemyTypeSelected, out ObjectPool<EnemyCharacter> poolSelected))
            return poolSelected;

        return null;
    }

    private EnemyCharacter GetEnemy(Vector3 spawnPosition)
    {
        EnemyCharacter enemy = _selectedEnemyPool.GetPoolObject();

        if (enemy == null) 
            return null;

        int additionalValue = 1;
        _currentEnemyOnScene += additionalValue;
        Debug.Log("GetEnemy / CurrentEnemyOnScene == " + _currentEnemyOnScene);

        enemy.Initialize();
        enemy.SetPool(_selectedEnemyPool);

        enemy.transform.position = spawnPosition;
        enemy.transform.rotation = Quaternion.Euler(MinRotationValue, Random.Range(MinRotationValue, MaxRotationValue), MinRotationValue);

        enemy.EnemyHealth.UnitDead += OnReturnEnemyToPool;

        return enemy;
    }

    private void OnReturnEnemyToPool(EnemyHealth enemyHealth)
    {
        Debug.Log("OnReturnEnemyToPool ");
        int decreasingValue = 1;

        EnemyCharacter enemy = enemyHealth.Enemy;

        _currentEnemyOnScene = _currentEnemyOnScene - decreasingValue;

        enemy.ReturnToPool(enemyHealth);

        enemyHealth.UnitDead -= OnReturnEnemyToPool;
    }
}
