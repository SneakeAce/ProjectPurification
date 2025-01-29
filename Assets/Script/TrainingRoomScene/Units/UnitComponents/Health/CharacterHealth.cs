using System;
using UnityEngine;

public class CharacterHealth : Health
{
    public event Action<float> MaxValueChanged;
    public event Action<float> CurrentValueChanged;

    public override void DamageTaken(float damage)
    {
        _currentValue -= damage;

        CurrentValueChanged?.Invoke(_currentValue);

        if (_currentValue <= 0)
            DestroyUnit();
    }

    public override void AddHealth(float value)
    {
        _currentValue += value;

        if (_currentValue > _maxValue)
            _currentValue = _maxValue;

        CurrentValueChanged?.Invoke(_currentValue);
    }

    public override void DestroyUnit()
    {
        Destroy(gameObject);
    }
}
