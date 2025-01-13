using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementStrategyFactory
{
    private Character _target;
    private List<Vector3> _patrolPoints;

    public EnemyMovementStrategyFactory(Character target)
    {
        _target = target;
    }

    public EnemyMovementStrategyFactory(List<Vector3> patrolPoints)
    {
        _patrolPoints = new List<Vector3>(patrolPoints);
    }

    public IBehavioralPattern Get(MoveTypes type, IMovable movable)
    {
        switch (type)
        {
            case MoveTypes.NoMove:
                return new NoMovePattern();

            case MoveTypes.MoveToTarget:
                return new MoveToTargetPattern(movable, _target);

            case MoveTypes.HoldPosition:
                Debug.Log("HoldPosition Pattern");
                return null;

            case MoveTypes.MoveShortDistance:
                Debug.Log("MoveShortDistance Pattern");
                return null;

            case MoveTypes.Patrol:
                Debug.Log("Patrol Pattern");
                return null;

            default:
                throw new ArgumentException(nameof(type));

        }
    }

}
