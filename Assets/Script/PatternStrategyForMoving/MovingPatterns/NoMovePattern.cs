using UnityEngine;

public class NoMovePattern : IBehavioralPattern
{
    private const float RandomTimeBetweenRotating = 30f;

    private IMovable _movable;

    private float _timeBetweenRotating;
    private float _rotatingSpeed = 100f;

    private bool _isRotating;

    private Quaternion _rotate;

    public NoMovePattern(IMovable movable)
    {
        _movable = movable;
    }

    public void StartMove()
    {
        _timeBetweenRotating = Random.Range(1f, RandomTimeBetweenRotating);
        _movable.NavMeshAgent.isStopped = false;
    }

    public void StopMove()
    {
        _movable.NavMeshAgent.isStopped = true;
    }

    public void Update()
    {
        if (_timeBetweenRotating > 0)
        {
            _timeBetweenRotating -= Time.deltaTime;
            return;
        }

        RandomRotate();
    }

    private void RandomRotate()
    {
        if (_isRotating == false)
        {
            float randomDirection = Random.Range(0f, 360f);

            _rotate = Quaternion.Euler(0f, randomDirection, 0f);

            _isRotating = true;

            _movable.Animator.SetBool("IsRotating", _isRotating);
        }

        _movable.Transform.rotation = Quaternion.RotateTowards(_movable.Transform.rotation, _rotate, _rotatingSpeed * Time.deltaTime);   

        if (Quaternion.Angle(_movable.Transform.rotation, _rotate) <= 1f)
        {
            _timeBetweenRotating = Random.Range(1f, RandomTimeBetweenRotating);
            _isRotating = false;

            _movable.Animator.SetBool("IsRotating", _isRotating);
        }
    }
}
