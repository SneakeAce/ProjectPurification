using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatrolPattern : IBehavioralPattern
{
    private const float MinDistanceToPoint = 0.11f;
    private const float MaxDistanceToPoint = 1.5f;

    private IEnemy _movable;

    private Transform _currentPoint;

    private SpawnPatrolPoints _spawnPatrolPoints;
    private SwitchBehavioralPattern _switchBehavioral;

    private List<Transform> _patrolPoints = new List<Transform>();

    private float _startTimeBetweenGetNewPoint = 5f;
    private float _timeBetweenGetNewPoint;

    private bool _isMoving;
    private bool _hasPointReached;

    public PatrolPattern(IEnemy movable, SpawnPatrolPoints spawnPatrolPoints, SwitchBehavioralPattern switchBehavioral)
    {
        _switchBehavioral = switchBehavioral;

        _movable = movable;
        _movable.NavMeshAgent.stoppingDistance = 0;

        _spawnPatrolPoints = spawnPatrolPoints;

        _patrolPoints.Clear();

        _patrolPoints = _spawnPatrolPoints.GetPatrolPoints();

        if (_patrolPoints.Count > 0)
            _currentPoint = _patrolPoints[Random.Range(0, _patrolPoints.Count)];

        //_movable.EnemyHealth.UnitDead += OnDeleteAllPoints;
    }

    public void StartMove()
    {
        _timeBetweenGetNewPoint = _startTimeBetweenGetNewPoint;

        if (_patrolPoints.Count > 0)
        {
            _isMoving = true;
            _hasPointReached = false;

            _movable.NavMeshAgent.isStopped = false;
        }
    }

    public void StopMove()
    {
        _isMoving = false;
        _hasPointReached = false;

        _movable.NavMeshAgent.isStopped = true;
        //OnDeleteAllPoints(_movable.EnemyHealth);

        if (_patrolPoints.Count > 0)
            _patrolPoints.Clear();
    }

    public void Update()
    {
        if (_currentPoint == null)
            return;

        if (Vector3.Distance(_movable.Transform.position, _currentPoint.transform.position) <= MinDistanceToPoint)
        {
            Debug.Log("HasPointReached");

            _hasPointReached = true;

            GetNewPoint();
        }

        if (_hasPointReached)
        {
            Debug.Log("HasPointReached StoppingMove");
            StoppingMove();
        }

        if (_movable.NavMeshAgent.isStopped && _hasPointReached == false) 
            RepeatCheckDistance();
        else if (_movable.NavMeshAgent.isStopped && _hasPointReached)
            return;

        _movable.NavMeshAgent.SetDestination(_currentPoint.transform.position);

        _movable.Animator.SetBool("IsRunning", _isMoving);
    }

    private void StoppingMove()
    {
        _isMoving = false;
        _hasPointReached = false;

        _movable.NavMeshAgent.isStopped = true;

        _movable.Animator.SetBool("IsRunning", _isMoving);
    }

    private void GetNewPoint()
    {
        if (_timeBetweenGetNewPoint > 0)
        { 
            _timeBetweenGetNewPoint -= Time.deltaTime;
            return;
        }

        if (_patrolPoints.Count > 0)
        {
            if (_patrolPoints.Contains(_currentPoint))
            {
                _patrolPoints.Remove(_currentPoint);
                _spawnPatrolPoints.DestroyPoint(_currentPoint.gameObject);

                if (_patrolPoints.Count == 0)
                {
                    int willBehaviorChange = Random.Range(Convert.ToInt32(true), Convert.ToInt32(false));

                    bool behaviorChanged = Convert.ToBoolean(willBehaviorChange);

                    if (behaviorChanged)
                    {
                        _switchBehavioral.SetBehavioralPattern(_movable);

                        StopMove();

                        return;
                    }
                    else
                    {
                        GetNewPatrolPoints();

                        if (_patrolPoints.Count == 0)
                        {
                            Debug.LogWarning("Ќе удалось создать новые точки патрулировани€!");
                            StopMove();
                            return;
                        }
                    }
                }
            }

            _currentPoint = _patrolPoints[Random.Range(0, _patrolPoints.Count)];

            StartMove();
        }

    }

    private void RepeatCheckDistance()
    {
        if (Vector3.Distance(_movable.Transform.position, _currentPoint.transform.position) <= MaxDistanceToPoint)
        {
            _hasPointReached = true;

            GetNewPoint();
        }
    }

    private void GetNewPatrolPoints()
    {
        _patrolPoints.Clear();

        _patrolPoints = _spawnPatrolPoints.GetPatrolPoints();
    }

    private void OnDeleteAllPoints(EnemyHealth enemyHealth)
    {
        if (_patrolPoints.Count > 0)
        {
            foreach (Transform patrolPoint in _patrolPoints)
            {
                if (_patrolPoints.Contains(patrolPoint))
                {
                    _patrolPoints.Remove(patrolPoint);
                    _spawnPatrolPoints.DestroyPoint(patrolPoint.gameObject);
                }
            }

            _patrolPoints.Clear();
        }

        if (enemyHealth.IsAlive == false)
            enemyHealth.UnitDead -= OnDeleteAllPoints;
    }
}
