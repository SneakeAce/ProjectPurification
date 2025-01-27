using System;

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
            case MoveTypes.NoMove:
                return new NoMovePattern(movable);
                
            case MoveTypes.MoveToTarget:
                return new MoveToTargetPattern(movable, _target);
                
            case MoveTypes.Patrol:
                return new PatrolPattern(movable, _spawnPatrolPoints);

            //case MoveTypes.MoveShortDistance:
                //Debug.Log("MoveShortDistance Pattern");
               // return null;

            default:
                throw new ArgumentException(nameof(type));

        }
    }

}
