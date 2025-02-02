using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnPatrolPoints : MonoBehaviour
{
    private const int MaxAttemptForCheckSpawnPoint = 10;
    private const float TimeBeforeDestroyPoint = 0.8f;

    [SerializeField] private GameObject _pointPrefab;

    [SerializeField] private LayerMask _pointLayer;
    [SerializeField] private LayerMask _obstacleLayer;

    [SerializeField] private int _maxPatrolPoints;
    [SerializeField] private float _maxRadiusSpawnPoint;

    [SerializeField] private float _radiusColliderPoint = 10f;

    private List<Transform> _patrolPoints;

    private Coroutine _destroyPointCoroutine;

    public void DestroyPoint(GameObject item)
    {
        if (_destroyPointCoroutine != null)
        {
            StopCoroutine(_destroyPointCoroutine);
            _destroyPointCoroutine = null;
        }

        _destroyPointCoroutine = StartCoroutine(DestroyPointJob(item));
    }

    public List<Transform> GetPatrolPoints()
    {
        _patrolPoints = new List<Transform>();

        for (int currentPatrolPoints = 0; currentPatrolPoints < _maxPatrolPoints;)
        {
            Vector3 point = RandomPosition();

            if (point != Vector3.zero)
            {
                GameObject instancePatrolPoint = Instantiate(_pointPrefab, point, Quaternion.identity);

                Debug.Log("instancePatrolPoint = " + instancePatrolPoint);
                currentPatrolPoints++;

                _patrolPoints.Add(instancePatrolPoint.transform);
            }
        }

        return _patrolPoints;
    }

    private IEnumerator DestroyPointJob(GameObject item)
    {
        yield return new WaitForSeconds(TimeBeforeDestroyPoint);

        if (item != null)
            Destroy(item);

        StopCoroutine(_destroyPointCoroutine);
        _destroyPointCoroutine = null;
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
        Collider[] pointsInRadius = Physics.OverlapSphere(newPointPosition, _radiusColliderPoint, _pointLayer);

        if (pointsInRadius.Length > 0)
            return false;

        return true;
    }

    private bool CheckObstacleAroundPoint(Vector3 newPointPosition)
    {
        Collider[] obstacleInRadius = Physics.OverlapSphere(newPointPosition, _radiusColliderPoint, _obstacleLayer);

        if (obstacleInRadius.Length > 0)
            return false;

        return true;
    }
}
