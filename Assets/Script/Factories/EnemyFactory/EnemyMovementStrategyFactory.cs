using System;
using System.Diagnostics;

public class EnemyMovementStrategyFactory
{
    private ICharacter _target;
    private PatrolPointsSpawner _spawnPatrolPoints;

    public EnemyMovementStrategyFactory()
    {
    }

    public EnemyMovementStrategyFactory(ICharacter target)
    {
        _target = target;
    }

    public EnemyMovementStrategyFactory(PatrolPointsSpawner spawnPatrolPoints)
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
