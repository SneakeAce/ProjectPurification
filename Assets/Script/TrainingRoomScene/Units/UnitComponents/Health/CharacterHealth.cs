using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] protected float _maxValue;
    protected float _currentValue;

    public float MaxValue { get => _maxValue; set => _maxValue = value; }
    public float CurrentValue { get => _currentValue; set => _currentValue = value; }

    public event Action<float> MaxValueChanged;
    public event Action<float> CurrentValueChanged;

    public void Initialize()
    {
        _currentValue = _maxValue;
    }

    public void DamageTaken(float damage)
    {
        _currentValue -= damage;

        CurrentValueChanged?.Invoke(_currentValue);

        if (_currentValue <= 0)
            DestroyUnit();
    }

    public void AddHealth(float value)
    {
        _currentValue += value;

        if (_currentValue > _maxValue)
            _currentValue = _maxValue;

        CurrentValueChanged?.Invoke(_currentValue);
    }

    public void DestroyUnit()
    {
        Destroy(gameObject);
    }
}
