using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatrolPattern : IBehavioralPattern
{
    private const int RandomBooleanMin = 0;
    private const int RandomBooleanMaxExclusive = 2;
    private const int RandomBooleanTrueValue = 1;

    private const int NonEmptyListThreshold = 0;
    private const int RandomRangeStart = 0;
    private const int MinStoppingDistance = 0;

    private const float MinTimeBetweenGetNewPoint = 0f;
    private const float MinDistanceToPoint = 0.09f;
    private const float MaxDistanceToPoint = 2f;

    private IEnemy _movable;

    private Transform _currentPoint;

    private PatrolPointsSpawner _spawnPatrolPoints;
    private BehavioralPatternSwitcher _switchBehavioral;

    private List<Transform> _patrolPoints = new List<Transform>();

    private float _startTimeBetweenGetNewPoint = 5f;
    private float _timeBetweenGetNewPoint;

    private bool _isMoving;
    private bool _hasPointReached;

    public PatrolPattern(IEnemy movable, PatrolPointsSpawner spawnPatrolPoints)
    {
        _switchBehavioral = movable.BehavioralPatternSwitcher;

        _movable = movable;
        _movable.NavMeshAgent.stoppingDistance = MinStoppingDistance;

        _spawnPatrolPoints = spawnPatrolPoints;

        _patrolPoints.Clear();

        _patrolPoints = _spawnPatrolPoints.GetPatrolPoints(_movable);

        if (_patrolPoints.Count > NonEmptyListThreshold) 
            _currentPoint = _patrolPoints[Random.Range(RandomRangeStart, _patrolPoints.Count)];

        //_movable.EnemyHealth.UnitDead += OnDeleteAllPoints;
    }

    public void StartMove()
    {
        _timeBetweenGetNewPoint = _startTimeBetweenGetNewPoint;

        if (_patrolPoints.Count > NonEmptyListThreshold)
        {
            _isMoving = true;
            _hasPointReached = false;

            _movable.NavMeshAgent.isStopped = false;

            _movable.NavMeshAgent.SetDestination(_currentPoint.transform.position);

            _movable.Animator.SetBool("IsRunning", _isMoving);
        }
    }

    public void StopMove()
    {
        _isMoving = false;
        _hasPointReached = false;

        _movable.NavMeshAgent.isStopped = true;
        //OnDeleteAllPoints(_movable.EnemyHealth);

        if (_patrolPoints.Count > NonEmptyListThreshold)
            _patrolPoints.Clear();
    }

    public void Update()
    {
        if (_currentPoint == null)
            return;

        if (Vector3.Distance(_movable.Transform.position, _currentPoint.transform.position) <= MinDistanceToPoint)
        {
            _hasPointReached = true;

            GetNewPoint();
        }

        if (_hasPointReached)
            StoppingMove();
        

        if (_movable.NavMeshAgent.isStopped && _hasPointReached == false) 
            RepeatCheckDistance();
        else if (_movable.NavMeshAgent.isStopped && _hasPointReached)
            return;
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
        if (_timeBetweenGetNewPoint > MinTimeBetweenGetNewPoint)
        { 
            _timeBetweenGetNewPoint -= Time.deltaTime;
            return;
        }

        if (_patrolPoints.Count > NonEmptyListThreshold && _patrolPoints.Contains(_currentPoint))
        {
            _patrolPoints.Remove(_currentPoint);
            _spawnPatrolPoints.DestroyPoint(_currentPoint.gameObject);

            if (_patrolPoints.Count == NonEmptyListThreshold)
            {
                Debug.Log("GetNewPoint");

                TryChangeBehaviorOrRefreshPatrol();
            }

            if (_patrolPoints.Count == NonEmptyListThreshold)
                return;

            _currentPoint = _patrolPoints[Random.Range(RandomRangeStart, _patrolPoints.Count)];

            StartMove();
        }

    }

    private void TryChangeBehaviorOrRefreshPatrol()
    {
        bool hasBehaviorChanged = Random.Range(RandomBooleanMin, RandomBooleanMaxExclusive) == RandomBooleanTrueValue;

        if (hasBehaviorChanged)
        {
            SetNoMovePattern();

            return;
        }
        else
        {
            GetNewListPatrolPoints();

            if (_patrolPoints.Count == NonEmptyListThreshold)
            {
                SetNoMovePattern();

                return;
            }
        }

    }

    private void SetNoMovePattern()
    {
        _switchBehavioral.SetBehavioralPattern(_movable, MoveTypes.NoMove);

        StopMove();
    }

    private void RepeatCheckDistance()
    {
        if (Vector3.Distance(_movable.Transform.position, _currentPoint.transform.position) <= MaxDistanceToPoint)
        {
            _hasPointReached = true;

            GetNewPoint();
        }
    }

    private void GetNewListPatrolPoints()
    {
        _patrolPoints.Clear();

        _patrolPoints = _spawnPatrolPoints.GetPatrolPoints(_movable);
    }

    private void OnDeleteAllPoints(IEnemy enemy)
    {
        if (_patrolPoints.Count > NonEmptyListThreshold)
        {
            // Переделать в for цикл!
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

        //if (enemy.CharacterEnemy.EnemyHealth.IsAlive == false)
        //    enemy.CharacterEnemy.EnemyHealth.UnitDead -= OnDeleteAllPoints;
    }
}
