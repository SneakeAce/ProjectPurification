using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const float RadiusSpawnNewEnemy = 6f;
    private const int AttemptsForSearchNewPoint = 5;

    [Header("Spawn Point and Enemy prefab")]
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    [SerializeField] private List<GameObject> _enemiesPrefabs;
    [SerializeField] private LayerMask _enemyLayer;

    [Header("Parameters")]
    [SerializeField] private float _radiusSpawn;
    [SerializeField] private float _startTimeBetweenSpawn;
    [SerializeField] private int _maxEnemyOnScene;

    private float _timeBetweenSpawn;

    private int _currentEnemyOnScene;

    private List<GameObject> _enemies = new List<GameObject>();

    private void Start()
    {
        _timeBetweenSpawn = _startTimeBetweenSpawn;
    }

    private void Update()
    {
        if (_timeBetweenSpawn > 0)
            _timeBetweenSpawn -= Time.deltaTime;

        if (_currentEnemyOnScene < _maxEnemyOnScene && _timeBetweenSpawn <= 0)
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        float directionLook = Random.Range(0f, 360f);

        Vector3 newPositionEnemy = GetSpawnPoint();

        GameObject instanceEnemy = Instantiate(_enemiesPrefabs[Random.Range(0, _enemiesPrefabs.Count)], newPositionEnemy, Quaternion.Euler(0, directionLook, 0));
        _currentEnemyOnScene++;

        EnemyCharacter enemy = instanceEnemy.GetComponent<EnemyCharacter>();

        // «десь будет задаватьс€ передвижени€ дл€ врага.

        _enemies.Add(enemy.gameObject);

        _timeBetweenSpawn = _startTimeBetweenSpawn;
    }

    private Vector3 GetSpawnPoint()
    {
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

        for (int attempt = 0; attempt < AttemptsForSearchNewPoint; attempt++)
        {
            Vector3 newPositionEnemy = (Random.insideUnitSphere * _radiusSpawn) + spawnPoint.transform.position;
            newPositionEnemy.y = 0;

            if (CheckEnemyInSpawnRadius(newPositionEnemy))
                return newPositionEnemy;
        }

        return Vector3.zero;
    }

    private bool CheckEnemyInSpawnRadius(Vector3 spawnPointPosition)
    {
        Collider[] enemyInRadius = Physics.OverlapSphere(spawnPointPosition, _radiusSpawn);
        foreach(Collider enemy in enemyInRadius)
        {
            if (enemy != null && enemy.gameObject.layer == _enemyLayer && _enemies.Count > 0)
            { 
                for (int i = 0; i < _enemies.Count; i++)
                {
                    if (Vector3.Distance(enemy.transform.position, _enemies[i].transform.position) < RadiusSpawnNewEnemy)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}
