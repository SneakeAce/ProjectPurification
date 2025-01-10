using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharacter : Unit, IMovable
{
    private IBehavioralPattern _behavioralPattern;
    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _holderAttackLogic;

    public float MoveSpeed => _moveSpeed;
    public Animator EnemyAnimator => Animator;
    public Rigidbody EnemyRigidbody => Rigidbody;
    public Transform Transform => transform;
    public NavMeshAgent NavMeshAgent => _agent;

    private void Update()
    {
        if (_behavioralPattern != null)
            _behavioralPattern.Update();
    }

    public void TriggerAttack()
    {
        BaseZombieAttack attack = _holderAttackLogic.GetComponent<BaseZombieAttack>();

        attack.AnimationAttack();
    }

    public void SetBehavioralPattern(IBehavioralPattern behavioralPattern)
    {
        _behavioralPattern?.StopMove();

        _behavioralPattern = behavioralPattern;

        _behavioralPattern.StartMove();
    }
}
