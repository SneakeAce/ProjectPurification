using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected float _maxValue;
    protected float _currentValue;

    public float MaxValue { get => _maxValue; set => _maxValue = value; }
    public float CurrentValue { get => _currentValue; set => _currentValue = value; }

    public void Initialize()
    {
        _currentValue = _maxValue;
    }

    public abstract void DamageTaken(float damage);

    public abstract void AddHealth(float value);

    public abstract void DestroyUnit();
}
