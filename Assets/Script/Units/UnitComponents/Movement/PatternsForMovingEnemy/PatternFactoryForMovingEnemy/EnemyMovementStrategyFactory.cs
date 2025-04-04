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

    public IBehavioralPattern Get(MoveTypes type, IEnemy movable, BehavioralPatternSwitcher switchBehavioral)
    {
        switch (type)
        {
            case MoveTypes.NoMove:
                return new NoMovePattern(movable, switchBehavioral);
                
            case MoveTypes.Patrol:
                return new PatrolPattern(movable, _spawnPatrolPoints, switchBehavioral);

            case MoveTypes.MoveToTarget:
                return new MoveToTargetPattern(movable, _target);
                
            default:
                throw new ArgumentException(nameof(type));

        }
    }

}
