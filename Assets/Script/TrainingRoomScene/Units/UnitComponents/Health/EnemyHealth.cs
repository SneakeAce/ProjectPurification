using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] protected float _maxValue;
    protected float _currentValue;

    private bool _isAlive;
    private EnemyCharacter _enemy;

    public float MaxValue { get => _maxValue; set => _maxValue = value; }
    public float CurrentValue { get => _currentValue; set => _currentValue = value; }

    public EnemyCharacter Enemy { get => _enemy; set => _enemy = value; }
    public bool IsAlive => _isAlive;


    public event Action<EnemyHealth> UnitDead;

    public void Initialize(EnemyCharacter enemy)
    {
        _enemy = enemy;

        _currentValue = _maxValue;

        _isAlive = true;
    }


    public void DamageTaken(float damage)
    {
        _currentValue -= damage;

        if (_currentValue <= 0)
        {
            _isAlive = false;

            UnitDead?.Invoke(this);
        }
    }

    public void AddHealth(float value)
    {
        
    }
}
