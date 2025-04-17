using UnityEngine;

public class MoveToTargetPattern : IBehavioralPattern
{
    private Character _target;
    private IEnemy _movable;

    private const float MinDistanceToTarget = 0.12f; 

    public MoveToTargetPattern(IEnemy movable, Character target)
    {
        _movable = movable;
        _target = target;

        Debug.Log("MoveToTargetPattern Construct / target = " + _target);

        _movable.NavMeshAgent.stoppingDistance = 1.5f;
    }

    public void StartMove()
    {
        _movable.NavMeshAgent.isStopped = false;
    }

    public void StopMove() 
    {
       _movable.NavMeshAgent.SetDestination(_movable.Transform.position + new Vector3(MinDistanceToTarget, 0.0f, MinDistanceToTarget));

        _movable.NavMeshAgent.isStopped = true;

        _movable.Animator.SetBool("IsRunning", false);
    }

    public void Update()
    {
        if (_target == null)
            return;

        if (_movable.NavMeshAgent.remainingDistance <= _movable.NavMeshAgent.stoppingDistance &&
            _movable.NavMeshAgent.hasPath && _movable.NavMeshAgent.isStopped == false)
        {
            StopMove();
            return;
        }
        else if (Vector3.Distance(_movable.Transform.position, _target.transform.position) > _movable.NavMeshAgent.stoppingDistance && _movable.NavMeshAgent.isStopped)
        {
            StartMove();
        }

        if (_movable.NavMeshAgent.isStopped)
            return;

        _movable.NavMeshAgent.SetDestination(_target.transform.position);

        _movable.Animator.SetBool("IsRunning", true);
    }
}
