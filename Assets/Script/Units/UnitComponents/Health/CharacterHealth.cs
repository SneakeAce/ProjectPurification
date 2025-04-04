using System;

public class CharacterHealth
{
    private const float MinCurrentHealth = 0f;

    protected float _maxValue;
    protected float _currentValue;

    private Character _character;

    public float MaxValue { get => _maxValue; set => _maxValue = value; }
    public float CurrentValue { get => _currentValue; set => _currentValue = value; }

    public event Action<float> MaxValueChanged;
    public event Action<float> CurrentValueChanged;
    public event Action<Character> OnDead;

    public void Initialization(Character character)
    {
        _character = character;
        _currentValue = _maxValue = _character.PlayerConfig.MaxHealth; 
    }

    public void DamageTaken(float damage)
    {
        _currentValue -= damage;

        CurrentValueChanged?.Invoke(_currentValue);

        if (_currentValue <= MinCurrentHealth)
            OnDead?.Invoke(_character);
    }

    public void AddHealth(float value)
    {
        _currentValue += value;

        if (_currentValue > _maxValue)
            _currentValue = _maxValue;

        CurrentValueChanged?.Invoke(_currentValue);
    }

}
