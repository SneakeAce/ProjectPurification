using System;
using System.Diagnostics;

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
        UnityEngine.Debug.Log("EnemyMovementStrategyFactory Contruct / target = " + target);
    }

    public EnemyMovementStrategyFactory(SpawnPatrolPoints spawnPatrolPoints)
    {
        _spawnPatrolPoints = spawnPatrolPoints;
    }

    public IBehavioralPattern Get(MoveTypes type, IEnemy movable)
    {
        switch (type)
        {
            case MoveTypes.NoMove:
                return new NoMovePattern(movable);
                
            case MoveTypes.Patrol:
                return new PatrolPattern(movable, _spawnPatrolPoints);

            case MoveTypes.MoveToTarget:
                return new MoveToTargetPattern(movable, _target);
                
            default:
                throw new ArgumentException(nameof(type));

        }
    }

}
