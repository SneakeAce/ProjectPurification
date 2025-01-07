using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPatrolPoints : MonoBehaviour
{
    [SerializeField] private int _maxPatrolPoints;
    [SerializeField] private float _maxRadiusSpawnPoint;

    private GameObject _pointForPatrol;

    private Vector3 _currentPoint;
    private Vector3 _lastPoint;

    private List<Vector3> _pointPositions;
    private List<Transform> _patrolPoints;

    public List<Transform> GetPatrolPoints()
    {
        for (int i = 0; i < _maxPatrolPoints; i++)
        {
            _currentPoint = RandomPosition();

            if (_currentPoint == _lastPoint)
            {
                _currentPoint = Vector3.zero;
                _currentPoint = RandomPosition();
            }

            SpawnPoint(_currentPoint);

            _lastPoint = _currentPoint;
        }

        return _patrolPoints;
    }

    private Vector3 RandomPosition()
    {
        //Vector3 spawnPosition = new Vector3(Random.Range(-_maxRadiusSpawnPoint, _maxRadiusSpawnPoint), 0, Random.Range(-_maxRadiusSpawnPoint, _maxRadiusSpawnPoint));
        Vector3 spawnPosition = Random.insideUnitSphere * _maxRadiusSpawnPoint;
        spawnPosition.y = 0;

        _pointPositions.Add(spawnPosition);

        return spawnPosition;
    }
    
    private void SpawnPoint(Vector3 position)
    {
        GameObject instancePointForPatrol = Instantiate(_pointForPatrol, position, Quaternion.identity);
        _patrolPoints.Add(instancePointForPatrol.transform);
    }
}
