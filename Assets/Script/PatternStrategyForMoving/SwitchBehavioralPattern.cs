using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwitchBehavioralPattern : MonoBehaviour
{
    [SerializeField] private SearchTarget _searchTarget;
    [SerializeField] private SpawnPatrolPoints _spawnPatrolPoints;

    private List<MoveTypes> _moveTypes = new List<MoveTypes>();

    private Character _target;
    private EnemyCharacter _enemyCharacter;

    private EnemyMovementStrategyFactory _movementFactory;
    private MoveTypes _currentMoveTypes;

    private bool _isStartingSearchTarget;

    public void SetBehavioralPattern(EnemyCharacter enemy)
    {
        _enemyCharacter = enemy;

        MoveTypes moveType = GetRandomMoveType();

        SetMoveType(moveType);
    }

    private void Update()
    {
        if (_isStartingSearchTarget == false)
        {
            StartSearchingTarget();
        }
    }

    private void SetMoveType(MoveTypes moveType)
    {
        _currentMoveTypes = moveType;

        _enemyCharacter.SetBehavioralPattern(_movementFactory.Get(moveType, _enemyCharacter));
    }

    private void StartSearchingTarget()
    {
        _searchTarget.TargetFound += OnTargetFound;

        _searchTarget.StartSearchingTarget();

        _isStartingSearchTarget = true;
    }

    private void OnTargetFound()
    {
        _target = _searchTarget.Target; 
        
        _movementFactory = new EnemyMovementStrategyFactory(_target);

        SetMoveType(MoveTypes.MoveToTarget);

        _searchTarget.TargetFound -= OnTargetFound;

        _searchTarget.TargetDisappeared += OnTargetDisapperead;
    }

    private void OnTargetDisapperead()
    {
        _target = null;

        SetMoveType(MoveTypes.NoMove);

        _searchTarget.TargetDisappeared -= OnTargetDisapperead;

        _isStartingSearchTarget = false;
    }

    private MoveTypes GetRandomMoveType()
    {
        if (_moveTypes.Count > 0)
            _moveTypes.Clear();

        _moveTypes = Enum.GetValues(typeof(MoveTypes)).Cast<MoveTypes>().ToList();

        MoveTypes moveType = _moveTypes[UnityEngine.Random.Range(0, _moveTypes.Count)];

        if (moveType == MoveTypes.MoveToTarget)
        {
            _movementFactory = new EnemyMovementStrategyFactory();
            return MoveTypes.NoMove;
        }
        else if (moveType == MoveTypes.Patrol)
        {
            _movementFactory = new EnemyMovementStrategyFactory(_spawnPatrolPoints);
            return moveType;
        }
        else
        {
            _movementFactory = new EnemyMovementStrategyFactory();
            return moveType;
        }
    }
}
