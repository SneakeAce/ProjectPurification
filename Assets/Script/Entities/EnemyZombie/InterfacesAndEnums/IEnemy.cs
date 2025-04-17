using UnityEngine;
using UnityEngine.AI;

public interface IEnemy : IEntity
{
    public EnemyType EnemyType { get; }
    float MoveSpeed { get; }
    Transform PatrolPointsHolder { get; }
    NavMeshAgent NavMeshAgent { get; }

    BehavioralPatternSwitcher BehavioralPatternSwitcher { get; }
    EnemyCharacter CharacterEnemy { get; }
    EnemyHealth EnemyHealth { get; }
}
