using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointsSpawner
{
    private const int MaxBufferSize = 16;
    private const int MaxAttemptForCheckSpawnPoint = 10;
    private const float TimeBeforeDestroyPoint = 0.2f;

    private const string PatrolPointsHolderName = "PatrolPointsHolder";

    private InstantiateAndDestroyGameObjectPerformer _gameObjectPerformer;
    private CoroutinePerformer _coroutinePerformer;
    private ParentForPatrolPointsHolder _parentForHolders;

    private GameObject _holderPatrolPoints;

    private List<Transform> _patrolPoints;
    private GameObject _pointPrefab;

    private Collider[] _bufferOtherPoints;
    private Collider[] _bufferObstacles;

    private LayerMask _anothersPointLayer;
    private LayerMask _obstacleLayer;

    private int _maxPatrolPoints;
    private float _maxRadiusSpawnPoint;

    private float _radiusCheckingAnotherNearestPoint;
    private float _radiusCheckingObstacle;

    private Coroutine _destroyPointCoroutine;

    public PatrolPointsSpawner(SpawnPatrolPointsConfig config, CoroutinePerformer coroutinePerformer, 
        InstantiateAndDestroyGameObjectPerformer gameObjectPermformer, ParentForPatrolPointsHolder parentForHolders)
    {
        _coroutinePerformer = coroutinePerformer;
        _gameObjectPerformer = gameObjectPermformer;
        _parentForHolders = parentForHolders;

        _pointPrefab = config.PointPrefab;

        _anothersPointLayer = config.AnothersPointLayer;
        _obstacleLayer = config.ObstacleLayer;

        _maxPatrolPoints = config.MaxPatrolPoints;
        _maxRadiusSpawnPoint = config.MaxRadiusSpawnPoint;

        _radiusCheckingAnotherNearestPoint = config.RadiusCheckingAnotherNearestPoint;
        _radiusCheckingObstacle = config.RadiusCheckingObstacle;
        
        _bufferOtherPoints = new Collider[MaxBufferSize];
        _bufferObstacles = new Collider[MaxBufferSize];
    }

    public void DestroyPoint(GameObject item)
    {
        if (_destroyPointCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_destroyPointCoroutine);
            _destroyPointCoroutine = null;
        }

        _destroyPointCoroutine = _coroutinePerformer.StartCoroutine(DestroyPointJob(item));
    }

    public List<Transform> GetPatrolPoints(IEnemy enemy)
    {
        _holderPatrolPoints = SpawnPatrolPointsHolder(enemy);
        _holderPatrolPoints.transform.SetParent(_parentForHolders.transform);

        _patrolPoints = new List<Transform>();

        for (int currentPatrolPoints = 0; currentPatrolPoints < _maxPatrolPoints;)
        {
            Vector3 point = RandomPosition();
            
            if (point != Vector3.zero)
            {
                GameObject instancePatrolPoint = _gameObjectPerformer.CreateObject(_pointPrefab, point, Quaternion.identity);
                currentPatrolPoints++;

                instancePatrolPoint.name = instancePatrolPoint.name + currentPatrolPoints.ToString();

                _patrolPoints.Add(instancePatrolPoint.transform);

                instancePatrolPoint.transform.SetParent(_holderPatrolPoints.transform);
            }
            else
            {
                Debug.Log("Не удалось создать точку. Вернулось Vector3.zero");
                break;
            }
        }

        return _patrolPoints;
    }

    private GameObject SpawnPatrolPointsHolder(IEnemy enemy)
    {
        string nameHolder = enemy.ToString() + PatrolPointsHolderName;
        GameObject holder = new GameObject(nameHolder.ToString());

        if (holder == null)
            return null;

        return holder;
    }

    private IEnumerator DestroyPointJob(GameObject item)
    {
        if (item != null)
        {
            _gameObjectPerformer.DestroyObject(item);
            _patrolPoints.Remove(item.transform);
        }

        if (_patrolPoints.Count == 0)
        {
            _gameObjectPerformer.DestroyObject(_holderPatrolPoints);
            _holderPatrolPoints = null;
        }

        yield return new WaitForSeconds(TimeBeforeDestroyPoint);
    }

    private Vector3 RandomPosition()
    {
        for (int attempt = 0; attempt < MaxAttemptForCheckSpawnPoint; attempt++)
        {
            Vector3 spawnPosition = Random.insideUnitSphere * _maxRadiusSpawnPoint;
            spawnPosition.y = 0;

            if (CheckOtherPointAround(spawnPosition) && CheckObstacleAroundPoint(spawnPosition))
              return spawnPosition;
        }

        return Vector3.zero;
    }

    private bool CheckOtherPointAround(Vector3 newPointPosition)
    {
        int pointsInRadius = Physics.OverlapSphereNonAlloc(newPointPosition, _radiusCheckingAnotherNearestPoint, _bufferOtherPoints, _anothersPointLayer);

        if (pointsInRadius > 0)
            return false;

        return true;
    }

    private bool CheckObstacleAroundPoint(Vector3 newPointPosition)
    {
        int obstacleInRadius = Physics.OverlapSphereNonAlloc(newPointPosition, _radiusCheckingObstacle, _bufferObstacles, _obstacleLayer);

        if (obstacleInRadius > 0)
            return false;

        return true;
    }
}
