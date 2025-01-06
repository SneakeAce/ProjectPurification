using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue;
    private float _currentValue;

    public float MaxValue { get => _maxValue; set => _maxValue = value; }
    public float CurrentValue { get => _currentValue; set => _currentValue = value; }

    public event Action<float> MaxValueChanged;
    public event Action<float> CurrentValueChanged;

    public void Initialize()
    {
        _currentValue = _maxValue;
    }

    // Для тестов!
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        AddHealth(10f);
    //    }        

    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        DamageTaken(10f);
    //    }
    //}

    public void DamageTaken(float damage)
    {
        _currentValue -= damage;

        CurrentValueChanged?.Invoke(_currentValue);
    }

    public void AddHealth(float value)
    {
        _currentValue += value;

        if (_currentValue > _maxValue)
            _currentValue = _maxValue;

        CurrentValueChanged?.Invoke(_currentValue);
    }

    // Метод для получения урона. 
    // В нем будет вызываться событие.

}
