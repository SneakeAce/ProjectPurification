using UnityEngine;
using UnityEngine.AI;

public interface IEnemy : IEntity
{
    public EnemyType EnemyType { get; }
    float MoveSpeed { get; }
    NavMeshAgent NavMeshAgent { get; }

    BehavioralPatternSwitcher BehavioralPatternSwitcher { get; }
    EnemyCharacter CharacterEnemy { get; }
}
