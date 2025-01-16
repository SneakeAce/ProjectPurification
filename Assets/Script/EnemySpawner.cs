using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const float RadiusSpawnNewEnemy = 50f;
    private const int AttemptsForSearchNewPoint = 5;

    [Header("Spawn Point and Enemy prefab")]
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    [SerializeField] private List<GameObject> _enemiesPrefabs;

    [Header("Enemy parameters")]
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _obstacleLayer;

    [Header("Parameters")]
    [SerializeField] private float _radiusCheckingEnemyAround;
    [SerializeField] private float _radiusCheckingObstacleAround;
    [SerializeField] private float _startTimeBetweenSpawn;
    [SerializeField] private int _maxEnemyOnScene;

    private float _timeBetweenSpawn;

    private int _currentEnemyOnScene;

    private SwitchBehavioralPattern _behavioralPattern;

    private List<GameObject> _enemies = new List<GameObject>();

    private void Start()
    {
        _timeBetweenSpawn = _startTimeBetweenSpawn;
    }

    private void Update()
    {
        _timeBetweenSpawn -= Time.deltaTime;

        if (_currentEnemyOnScene < _maxEnemyOnScene && _timeBetweenSpawn <= 0)
        {
            SpawnEnemy();
            _timeBetweenSpawn = _startTimeBetweenSpawn;
        }
    }

    private void SpawnEnemy()
    {
        float directionLook = Random.Range(0f, 360f);

        Vector3 newPositionEnemy = GetSpawnPoint();

        if (newPositionEnemy == Vector3.zero)
            return;

        GameObject instanceEnemy = Instantiate(_enemiesPrefabs[Random.Range(0, _enemiesPrefabs.Count)], newPositionEnemy, Quaternion.Euler(0, directionLook, 0));

        EnemyCharacter enemy = instanceEnemy.GetComponent<EnemyCharacter>();
        _behavioralPattern = enemy.GetComponentInChildren<SwitchBehavioralPattern>();

        _behavioralPattern.SetBehavioralPattern(enemy);

        _enemies.Add(enemy.gameObject);

        _currentEnemyOnScene++;

    }

    private Vector3 GetSpawnPoint()
    {
        for (int attempt = 0; attempt < AttemptsForSearchNewPoint; attempt++)
        {
            Vector3 newPositionEnemy = Random.insideUnitSphere * RadiusSpawnNewEnemy;
            newPositionEnemy.y = 0;

            Debug.Log("newPositionEnemy = " + newPositionEnemy);

            if (CheckEnemyInSpawnRadius(newPositionEnemy) && CheckObstacleInSpawnRadius(newPositionEnemy))
            {
                Debug.Log("newPositionEnemy = " + newPositionEnemy);
                return newPositionEnemy;
            }
        }

        Debug.Log("GetSpawnPoint / return Vector3.zero");

        return Vector3.zero;
    }

    private bool CheckEnemyInSpawnRadius(Vector3 spawnPointPosition)
    {
        Collider[] enemyInRadius = Physics.OverlapSphere(spawnPointPosition, _radiusCheckingEnemyAround, _enemyLayer);

        Debug.Log("CheckEnemyInSpawnRadius / enemyInRadius = " + enemyInRadius.Length);

        if (enemyInRadius.Length > 0)
            return false;

        return true;
    }    

    private bool CheckObstacleInSpawnRadius(Vector3 spawnPointPosition)
    {
        Collider[] obstacleInRadius = Physics.OverlapSphere(spawnPointPosition, _radiusCheckingObstacleAround, _obstacleLayer);

        Debug.Log("CheckObstacleInSpawnRadius / enemyInRadius = " + obstacleInRadius.Length);

        if (obstacleInRadius.Length > 0)
            return false;

        return true;
    }
}
