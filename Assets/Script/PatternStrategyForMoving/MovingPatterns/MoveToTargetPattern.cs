using UnityEngine;

public class MoveToTargetPattern : IBehavioralPattern
{
    private Character _target;
    private IMovable _movable;

    public MoveToTargetPattern(IMovable movable, Character target)
    {
        _movable = movable;
        _target = target;
    }

    public void StartMove()
    {
        _movable.NavMeshAgent.isStopped = false;
    }
    public void StopMove() => _movable.NavMeshAgent.isStopped = true;

    public void Update()
    {
        if (_movable.NavMeshAgent.remainingDistance <= _movable.NavMeshAgent.stoppingDistance && 
            _movable.NavMeshAgent.hasPath && _movable.NavMeshAgent.isStopped == false)
        {
            StopMove();
            _movable.Animator.SetBool("IsRunning", false);
            return;
        }
        else if (Vector3.Distance(_movable.Transform.position, _target.transform.position) > _movable.NavMeshAgent.stoppingDistance && _movable.NavMeshAgent.isStopped)
        {
            StartMove();
        }

        if (_target == null || _movable.NavMeshAgent.isStopped)
            return;

        _movable.NavMeshAgent.SetDestination(_target.transform.position);

        _movable.Animator.SetBool("IsRunning", true);
    }
}
