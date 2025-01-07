using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveToTargetPattern : IBehavioralPattern
{
    private const float MinDistanceToTarget = 1f;

    private Character _target;
    private IMovable _movable;

    private bool _isMoving;

    public MoveToTargetPattern(IMovable movable, Character target)
    {
        _movable = movable;
        _target = target;
    }

    public void StartMove() => _isMoving = true;

    public void StopMove() => _isMoving = false;

    public void Update()
    {
        if (Vector3.Distance(_movable.Transform.position, _target.transform.position) < MinDistanceToTarget)
        {
            StopMove();
            _movable.Animator.SetBool("IsRunning", false);
            return;
        }
        else
        {
            StartMove();
        }

        if (_target == null || _isMoving == false)
            return;


        Vector3 direction = _movable.Transform.position - _target.transform.position;
        _movable.Transform.Translate(direction.normalized * _movable.MoveSpeed * Time.deltaTime);
        _movable.Animator.SetBool("IsRunning", true);
    }
}
