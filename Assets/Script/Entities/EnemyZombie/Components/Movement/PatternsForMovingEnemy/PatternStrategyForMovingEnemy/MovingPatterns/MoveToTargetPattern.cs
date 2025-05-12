using UnityEngine;

public class MoveToTargetPattern : IBehavioralPattern
{
    private const float MinDistanceToTarget = 0.12f;

    private float _stoppingDistance;

    private IEntity _target;
    private IEnemy _movable;

    public MoveToTargetPattern(IEnemy movable, IEntity target)
    {
        if (target is ICharacter character)
        {
            _target = character;
            _stoppingDistance = 1.2f;
        }
        else if (target is ITurret turret)
        {
            _target = turret;
            _stoppingDistance = 1.85f;
        }
        else if (target is IBarrier barrier)
        {
            _target = barrier;
            _stoppingDistance = 1.2f;
        }

        _movable = movable;

        _target = target;

        _movable.NavMeshAgent.stoppingDistance = _stoppingDistance;
        Debug.Log("Start / " + '\n' + $" +  _movable.NavMeshAgent.stoppingDistance = {_movable.NavMeshAgent.stoppingDistance}");
    }

    public void StartMove()
    {
        _movable.NavMeshAgent.isStopped = false;

        _movable.Animator.SetBool("IsRunning", true);
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

        //if (Vector3.Distance(_movable.Transform.position, targetPos) < _movable.NavMeshAgent.stoppingDistance &&
        //    _movable.NavMeshAgent.isStopped == false)
        //{
        //    StopMove();
        //    return;
        //}

        if (_movable.NavMeshAgent.remainingDistance <= _movable.NavMeshAgent.stoppingDistance &&
            _movable.NavMeshAgent.hasPath && _movable.NavMeshAgent.isStopped == false)
        {
            StopMove();
            return;
        }
        else if (Vector3.Distance(_movable.Transform.position, targetPos) > _movable.NavMeshAgent.stoppingDistance && _movable.NavMeshAgent.isStopped)
        {
            Debug.Log("Else if");
            StartMove();
        }

        if (_movable.NavMeshAgent.isStopped)
            return;

        _movable.NavMeshAgent.SetDestination(targetPos);
    }
}
