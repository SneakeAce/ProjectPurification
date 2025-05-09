using UnityEngine;

public class MoveToTargetPattern : IBehavioralPattern
{
    private const float MinDistanceToTarget = 0.12f;
    private const float StoppingDistance = 1.5f;

    private IEntity _target;
    private IEnemy _movable;

    public MoveToTargetPattern(IEnemy movable, IEntity target)
    {
        _movable = movable;
        _target = target;

        _movable.NavMeshAgent.stoppingDistance = StoppingDistance;
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
        Vector3 targetPos = _target.Collider.bounds.ClosestPoint(_target.Transform.position);

        if (_target == null)
            return;

        if (_movable.NavMeshAgent.remainingDistance <= _movable.NavMeshAgent.stoppingDistance &&
            _movable.NavMeshAgent.hasPath && _movable.NavMeshAgent.isStopped == false)
        {
            StopMove();
            return;
        }
        else if (Vector3.Distance(_movable.Transform.position, targetPos) > _movable.NavMeshAgent.stoppingDistance && _movable.NavMeshAgent.isStopped)
        {
            StartMove();
        }

        if (_movable.NavMeshAgent.isStopped)
            return;

        _movable.NavMeshAgent.SetDestination(targetPos);

        _movable.Animator.SetBool("IsRunning", true);
    }
}
