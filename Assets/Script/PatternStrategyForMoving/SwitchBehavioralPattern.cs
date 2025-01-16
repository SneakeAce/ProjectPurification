using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwitchBehavioralPattern : MonoBehaviour
{
    private const float MaxAttemptForCheckMoveType = 5;

    [SerializeField] private SearchTarget _searchTarget;
    [SerializeField] private SpawnPatrolPoints _spawnPatrolPoints;

    private List<MoveTypes> _moveTypes = new List<MoveTypes>();

    private Character _target;
    private EnemyCharacter _enemyCharacter;

    private EnemyMovementStrategyFactory _movementFactory;

    public void SetBehavioralPattern(EnemyCharacter enemy)
    {
        _enemyCharacter = enemy;

        StartSearchingTarget();

        MoveTypes moveType = GetRandomMoveType();

        SetMoveType(moveType);
    }

    private void SetMoveType(MoveTypes moveType)
    {
        _enemyCharacter.SetBehavioralPattern(_movementFactory.Get(moveType, _enemyCharacter));
    }

    private void StartSearchingTarget()
    {
        _searchTarget.TargetFound += OnTargetFound;

        _searchTarget.StartSearchingTarget();
    }

    private void OnTargetFound()
    {
        _target = _searchTarget.Target; 
        
        _movementFactory = new EnemyMovementStrategyFactory(_target);

        SetMoveType(MoveTypes.MoveToTarget);

        _searchTarget.TargetFound -= OnTargetFound;
    }


    private MoveTypes GetRandomMoveType()
    {
        _moveTypes = Enum.GetValues(typeof(MoveTypes)).Cast<MoveTypes>().ToList();

        MoveTypes moveType = _moveTypes[UnityEngine.Random.Range(0, _moveTypes.Count)];

        //if (moveType == MoveTypes.MoveToTarget)
        //{
        //    _movementFactory = new EnemyMovementStrategyFactory();
        //    return MoveTypes.NoMove;
        //}
        //else if (moveType == MoveTypes.Patrol)
        //{
        //    _movementFactory = new EnemyMovementStrategyFactory(_spawnPatrolPoints);
        //    return moveType;
        //}
        //else
        //{
        //    _movementFactory = new EnemyMovementStrategyFactory();
        //    return moveType;
        //}

        _movementFactory = new EnemyMovementStrategyFactory(_spawnPatrolPoints);
        return MoveTypes.Patrol;
    }


    /*==================================DON'T USE YET======================================================================================*/

    /*
    private Coroutine _randomlyChooseMovePatterCoroutine;

    [SerializeField] private SpawnPatrolPoints _spawnPatrolPoints;

    private float _timeToChoose = 2f;

     _randomlyChooseMovePatterCoroutine = StartCoroutine(RandomlyChooseMovePatternJob());
    */

    /* Other Methods
      
    private void GetPoint()
    {
        _patrolPoints = new List<Transform>();
        _patrolPoints = _spawnPatrolPoints.GetPatrolPoints();
    }

    private IEnumerator RandomlyChooseMovePatternJob()
    {
        yield return new WaitForSeconds(_timeToChoose);


    }

    */
}
