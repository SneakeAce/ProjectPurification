using System;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;
using Zenject;

public class BehavioralPatternSwitcher : MonoBehaviour
{
    private SpawnPatrolPoints _spawnPatrolPoints;

    private Character _target;
    private EnemyCharacter _enemyCharacter;

    private EnemyMovementStrategyFactory _movementFactory;
    private BehavioralPatternSwitcher _switchBehavioralPattern;

    private MoveTypes _currentMoveTypes;
    private List<MoveTypes> _moveTypes = new List<MoveTypes>();

    [Inject]
    private void Construct(SpawnPatrolPoints spawnPatrolPoints)
    {
        _spawnPatrolPoints = spawnPatrolPoints;
    }

    public void SetBehavioralPattern(IEnemy enemy)
    {
        _switchBehavioralPattern = this;

        if (_enemyCharacter == null)
            _enemyCharacter = enemy.CharacterEnemy;

        MoveTypes moveType = GetRandomMoveType();

        SetMoveType(moveType);
    }

    public void SetBehavioralPattern(MoveTypes moveType, Character target = null)
    {
        if (target == null)
        {
            SetMoveType(moveType);
        }
        else
        {
            _movementFactory = new EnemyMovementStrategyFactory(_target);

            SetMoveType(moveType);
        }
    }

    private void SetMoveType(MoveTypes moveType)
    {
        _currentMoveTypes = moveType;

        _enemyCharacter.SetBehavioralPattern(_movementFactory.Get(_currentMoveTypes, _enemyCharacter, _switchBehavioralPattern));
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
                _movementFactory = new EnemyMovementStrategyFactory(_spawnPatrolPoints);
                return moveType;

            default:
                throw new Exception("Случилась ошибка в паттернах. Выбран не существующий");
        }
    }
}
