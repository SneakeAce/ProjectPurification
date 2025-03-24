using System.Collections;
using UnityEngine;
using Zenject;

public class LocalEnemySpawner : EnemySpawner
{
    private const float MinRotationValue = 0f;
    private const float MaxRotationValue = 360f;

    private float _radiusSpawn;

    private int _maxEnemyOnScene;
    private int _currentEnemyOnScene;

    private EnemyTypeInSpawner _enemyTypeInSpawner;

    private ObjectPool<EnemyCharacter> _selectedEnemyPool;

    private LocalEnemySpawnerConfig _config;

    [Inject]
    private void Construct(LocalEnemySpawnerConfig config)
    {
        _config = config;

        _enemyTypeInSpawner = config.EnemyTypeInSpawner;
        _radiusSpawn = config.RadiusSpawn;
        _maxEnemyOnScene = config.MaxEnemyOnSceneInCurrentLocalSpawner;

        _selectedEnemyPool = GetPool();
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
        if (_currentEnemyOnScene < _maxEnemyOnScene)
            return;

        Vector3 newPosition = GetSpawnPoint();

        if (newPosition == Vector3.zero)
            return;

        EnemyCharacter enemy = GetEnemy(newPosition);

        if (enemy == null)
            return;

        SwitchBehavioralPattern behavioralPattern = enemy.GetComponentInChildren<SwitchBehavioralPattern>();
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

    private ObjectPool<EnemyCharacter> GetPool()
    {
        EnemyType enemyTypeSelected = (EnemyType)_enemyTypeInSpawner;

        if (_enemiesPools.PoolDictionary.TryGetValue(enemyTypeSelected, out ObjectPool<EnemyCharacter> poolSelected))
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

        enemy.Initialize();
        enemy.SetPool(_selectedEnemyPool);

        enemy.transform.position = spawnPosition;
        enemy.transform.rotation = Quaternion.Euler(MinRotationValue, Random.Range(MinRotationValue, MaxRotationValue), MinRotationValue);

        enemy.EnemyHealth.UnitDead += OnReturnEnemyToPool;

        return enemy;
    }

    public override void OnReturnEnemyToPool(EnemyHealth enemyHealth)
    {
        int decreasingValue = 1;

        EnemyCharacter enemy = enemyHealth.Enemy;

        _currentEnemyOnScene = _currentEnemyOnScene - decreasingValue;

        enemy.ReturnToPool(enemyHealth);

        enemyHealth.UnitDead -= OnReturnEnemyToPool;
    }
}
