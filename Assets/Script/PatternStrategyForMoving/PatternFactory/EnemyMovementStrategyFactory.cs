using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementStrategyFactory
{
    private Character _target;
    private SpawnPatrolPoints _spawnPatrolPoints;

    public EnemyMovementStrategyFactory()
    {
    }

    public EnemyMovementStrategyFactory(Character target)
    {
        _target = target;
    }

    public EnemyMovementStrategyFactory(SpawnPatrolPoints spawnPatrolPoints)
    {
        _spawnPatrolPoints = spawnPatrolPoints;
    }

    public IBehavioralPattern Get(MoveTypes type, IMovable movable)
    {
        switch (type)
        {
            //case MoveTypes.None:
                //return null;

            case MoveTypes.NoMove:
                return new NoMovePattern();
                

            case MoveTypes.MoveToTarget:
                return new MoveToTargetPattern(movable, _target);
                

            //case MoveTypes.MoveShortDistance:
                //Debug.Log("MoveShortDistance Pattern");
               // return null;

            case MoveTypes.Patrol:
                return new PatrolPattern(movable, _spawnPatrolPoints);

            default:
                throw new ArgumentException(nameof(type));

        }
    }

}
