using UnityEngine;

public class NoMovePattern : IBehavioralPattern
{
    private const float RandomTimeBetweenRotating = 30f;
    private const float MinTimeBetweenRotating = 1f;

    private const float RandomTimeBetweenSwitchBehavioral = 2f;
    private const float MinTimeBetweenSwitchBehavioral = 1f;

    private IEnemy _movable;

    private float _timeBetweenSwitchBehavior;
    private float _timeBetweenRotating;
    private float _rotatingSpeed = 100f;

    private bool _isRotating;

    private BehavioralPatternSwitcher _switchBehavioral;

    private Quaternion _rotate;

    public NoMovePattern(IEnemy movable)
    {
        _switchBehavioral = movable.BehavioralPatternSwitcher;
        _movable = movable;
    }

    public void StartMove()
    {
        _timeBetweenSwitchBehavior = Random.Range(MinTimeBetweenSwitchBehavioral, RandomTimeBetweenSwitchBehavioral);

        _timeBetweenRotating = Random.Range(MinTimeBetweenRotating, RandomTimeBetweenRotating);

        _movable.NavMeshAgent.isStopped = false;
    }

    public void StopMove()
    {
        _movable.NavMeshAgent.isStopped = true;
    }

    public void Update()
    {
        if (_timeBetweenSwitchBehavior > 0)
        {
            _timeBetweenSwitchBehavior -= Time.deltaTime;
        }
        else
        {
            _switchBehavioral.SetBehavioralPattern(_movable);
        }

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
