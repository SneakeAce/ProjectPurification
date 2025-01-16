using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnPatrolPoints : MonoBehaviour
{
    private const int MaxAttemptForCheckSpawnPoint = 10;
    private const float MinRadiusAppearanceAnotherPoint = 5f;
    private const int TimeBeforeDestroyPoint = 1000;

    [SerializeField] private GameObject _pointPrefab;

    [SerializeField] private LayerMask _pointLayer;
    [SerializeField] private LayerMask _obstacleLayer;

    [SerializeField] private int _maxPatrolPoints;
    [SerializeField] private float _maxRadiusSpawnPoint;

    [SerializeField] private float _radiusColliderPoint = 10f;

    private List<Transform> _patrolPoints = new List<Transform>();

    public List<Transform> GetPatrolPoints()
    {
        for (int currentPatrolPoints = 0; currentPatrolPoints < _maxPatrolPoints; currentPatrolPoints++)
        {
            Vector3 point = RandomPosition();

            GameObject instancePatrolPoint = Instantiate(_pointPrefab, point, Quaternion.identity);

            if (instancePatrolPoint != null && point != Vector3.zero)
                _patrolPoints.Add(instancePatrolPoint.transform);
        }

        return _patrolPoints;
    }

    public async void DestroyPoint(GameObject item)
    {
        await Task.Delay(TimeBeforeDestroyPoint);

        if (item != null)
            Destroy(item);
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
