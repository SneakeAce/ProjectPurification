using System;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;
using Zenject;

public class BehavioralPatternSwitcher : MonoBehaviour
{
    private PatrolPointsSpawner _patrolPointsSpawner;

    private Character _target;
    private EnemyCharacter _enemyCharacter;

    private EnemyMovementStrategyFactory _movementFactory;

    private MoveTypes _currentMoveTypes;
    private List<MoveTypes> _moveTypes = new List<MoveTypes>();

    [Inject]
    private void Construct(PatrolPointsSpawner spawnPatrolPoints)
    {
        _patrolPointsSpawner = spawnPatrolPoints;
    }

    public void SetBehavioralPattern(IEnemy enemy)
    {
        if (_enemyCharacter == null)
            _enemyCharacter = enemy.CharacterEnemy;

        MoveTypes moveType = GetRandomMoveType();

        SetMoveType(moveType);
    }    
    
    public void SetBehavioralPattern(IEnemy enemy, MoveTypes type)
    {
        if (_enemyCharacter == null)
            _enemyCharacter = enemy.CharacterEnemy;

        MoveTypes moveType = type;

        SetMoveType(moveType);
    }

    public void SetBehavioralPattern(MoveTypes moveType, IEntity target = null)
    {
        if (target == null)
        {
            SetMoveType(moveType);
        }
        else
        {
            _movementFactory = new EnemyMovementStrategyFactory(target);

            SetMoveType(moveType);
        }
    }

    private void SetMoveType(MoveTypes moveType)
    {
        _currentMoveTypes = moveType;

        //Debug.Log("BehavioralPatternSwitcher / SetMoveType / moveType = " + moveType);

        _enemyCharacter.SetBehavioralPattern(_movementFactory.Get(_currentMoveTypes, _enemyCharacter));
    }

    private MoveTypes GetRandomMoveType()
    {
        if (_moveTypes.Count > 0)
            _moveTypes.Clear();

        _moveTypes = Enum.GetValues(typeof(MoveTypes)).Cast<MoveTypes>().ToList();

        MoveTypes moveType = _moveTypes[UnityEngine.Random.Range(0, _moveTypes.Count)];

        switch (moveType)
        {
            case MoveTypes.MoveToTarget:
                _movementFactory = new EnemyMovementStrategyFactory();
                return MoveTypes.NoMove;

            case MoveTypes.Patrol:
                _movementFactory = new EnemyMovementStrategyFactory(_patrolPointsSpawner);
                return moveType;

            case MoveTypes.NoMove:
                _movementFactory = new EnemyMovementStrategyFactory();
                return MoveTypes.NoMove;

            default:
                throw new Exception("Случилась ошибка в паттернах. Выбран не существующий");
        }
    }
}
