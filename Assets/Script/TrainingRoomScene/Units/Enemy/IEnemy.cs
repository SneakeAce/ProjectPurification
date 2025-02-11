using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemy
{
    float MoveSpeed { get; }
    Transform Transform { get; }
    NavMeshAgent NavMeshAgent { get; }
    Animator Animator { get; }
    EnemyCharacter CharacterEnemy { get; }
    EnemyHealth EnemyHealth { get; }
}
