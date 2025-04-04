using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemy
{
    public EnemyType EnemyType { get; }
    float MoveSpeed { get; }
    Transform Transform { get; }
    NavMeshAgent NavMeshAgent { get; }
    Animator Animator { get; }
    Rigidbody Rigidbody { get; }
    Collider Collider { get; }
    EnemyCharacter CharacterEnemy { get; }
    EnemyHealth EnemyHealth { get; }

}
