using UnityEngine;
using Zenject;

public class HealthBar : Bar
{
    [Inject] protected CharacterHealth _health;

    protected float _currentValue;
    protected float _maxValue;

    public override void Initialize()
    {
        _maxValue = _health.MaxValue;
        _currentValue = _health.CurrentValue;

        _health.CurrentValueChanged += OnChangedCurrentValue;
        _health.MaxValueChanged += OnChangedMaxValue;

        UpdateBar();
    }

    public override void UpdateBar()
    {
        _bar.value = _currentValue / _maxValue;

        _textMeshPro.text = Mathf.RoundToInt(_currentValue).ToString() + " / " + _maxValue.ToString();
    }

    public void OnChangedCurrentValue(float newValue)
    {
        _currentValue = newValue;

        UpdateBar();
    }

    public void OnChangedMaxValue(float newValue)
    {
        _maxValue = newValue;

        UpdateBar();
    }
}
