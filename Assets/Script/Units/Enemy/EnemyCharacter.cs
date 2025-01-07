using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Unit, IMovable
{
    private IBehavioralPattern _behavioralPattern;
    
    [SerializeField] private float _moveSpeed;

    public Transform Transform => transform;

    public float MoveSpeed => _moveSpeed;

    private void Update()
    {
        if (_behavioralPattern != null)
            _behavioralPattern.Update();
    }

    public void SetBehavioralPattern(IBehavioralPattern behavioralPattern)
    {
        _behavioralPattern?.StopMove();

        _behavioralPattern = behavioralPattern;

        _behavioralPattern.StartMove();
    }
}
