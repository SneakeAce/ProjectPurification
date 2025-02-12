using System.Collections;
using UnityEngine;

public abstract class EnemySpawner : MonoBehaviour
{
    protected const int AttemptsForSearchNewSpawnPoint = 5;

    [SerializeField] protected PoolCreator _createdPools;
    [SerializeField] protected LayerMask _enemyLayer;
    [SerializeField] protected LayerMask _obstacleLayer;
    [SerializeField] protected LayerMask _groundLayer;
                     
    [SerializeField] protected float _radiusCheckingEnemyAround;
    [SerializeField] protected float _radiusCheckingObstacleAround;
    [SerializeField] protected float _timeBetweenSpawn;

    protected bool _isCanWork = false;

    protected Coroutine _spawningCoroutine;
    protected SwitchBehavioralPattern _behavioralPattern;

    protected CreatedPoolEnemiesSystem PoolsEnemy => _createdPools.PoolEnemySystem;

    public abstract void SpawnEnemy();
    public abstract IEnumerator SpawningJob();
    public abstract bool CheckEnemyAroundSpawnPoint(Vector3 spawnPointPosition);
    public abstract bool CheckObstacleAroundSpawnPoint(Vector3 spawnPointPosition);
    public abstract bool CheckGroundUnderSpawnPoint(Vector3 spawnPointPosition);

    public virtual void Initialization()
    {
        Debug.Log("poolsEnemy = " + PoolsEnemy);
        _isCanWork = true;

        _spawningCoroutine = StartCoroutine(SpawningJob());
    }



    /* OldLogic
    private const int AttemptsForSearchNewPoint = 5;

    [Header("Spawn Point and Enemy prefab")]
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    [SerializeField] private List<GameObject> _enemiesPrefabs;

    [Header("Enemy parameters")]
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private LayerMask _groundLayer;

    [Header("Parameters")]
    [SerializeField] private float _radiusSpawnNewEnemy;
    [SerializeField] private float _radiusCheckingEnemyAround;
    [SerializeField] private float _radiusCheckingObstacleAround;
    [SerializeField] private float _startTimeBetweenSpawn;
    [SerializeField] private int _maxEnemyOnScene;

    private float _timeBetweenSpawn;

    private int _currentEnemyOnScene;

    private bool _isCanStartWork = false;

    private SwitchBehavioralPattern _behavioralPattern;

    private List<GameObject> _enemies = new List<GameObject>();

    public void Initialization()
    {
        _timeBetweenSpawn = _startTimeBetweenSpawn;

        _isCanStartWork = true;
    }

    private void Update()
    {
        if (_isCanStartWork == false)
            return;

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

        IMovable enemy = instanceEnemy.GetComponent<EnemyCharacter>();
        _behavioralPattern = enemy.EnemyCharacter.GetComponentInChildren<SwitchBehavioralPattern>();

        _behavioralPattern.SetBehavioralPattern(enemy);

        _enemies.Add(enemy.EnemyCharacter.gameObject);

        _currentEnemyOnScene++;

    }

    private Vector3 GetSpawnPoint()
    {
        for (int attempt = 0; attempt < AttemptsForSearchNewPoint; attempt++)
        {
            Vector3 newPositionEnemy = Random.insideUnitSphere * _radiusSpawnNewEnemy;
            newPositionEnemy.y = 0;

            if (CheckEnemyInSpawnRadius(newPositionEnemy) && CheckObstacleInSpawnRadius(newPositionEnemy) && CheckGroundUnderEnemy(newPositionEnemy))
            {
                return newPositionEnemy;
            }
        }

        return Vector3.zero;
    }

    private bool CheckEnemyInSpawnRadius(Vector3 spawnPointPosition)
    {
        Collider[] enemyInRadius = Physics.OverlapSphere(spawnPointPosition, _radiusCheckingEnemyAround, _enemyLayer);

        if (enemyInRadius.Length > 0)
            return false;

        return true;
    }    

    private bool CheckObstacleInSpawnRadius(Vector3 spawnPointPosition)
    {
        Collider[] obstacleInRadius = Physics.OverlapSphere(spawnPointPosition, _radiusCheckingObstacleAround, _obstacleLayer);

        if (obstacleInRadius.Length > 0)
            return false;

        return true;
    }

    private bool CheckGroundUnderEnemy(Vector3 spawnPointPosition)
    {
        Collider[] groundUnderEnemy = Physics.OverlapSphere(spawnPointPosition, _radiusCheckingObstacleAround, _groundLayer);

        if (groundUnderEnemy.Length > 0)
            return true;

        return false;
    }

    */
}
