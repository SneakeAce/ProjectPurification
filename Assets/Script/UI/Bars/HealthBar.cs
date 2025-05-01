using UnityEngine;
using Zenject;

public class HealthBar : Bar
{
    private CharacterHealth _health;

    //[Inject]
    //private void Construct(CharacterHealth health)
    //{
    //    _health = health;

    //    Initialize();
    //}

    public override void Initialize()
    {
        //_maxValue = _health.MaxValue;
        //_currentValue = _health.CurrentValue;

        //_health.CurrentValueChanged += OnChangedCurrentValue;
        //_health.MaxValueChanged += OnChangedMaxValue;

        //UpdateBar();
    }

    public override void UpdateBar()
    {
        //_bar.value = _currentValue / _maxValue;

        //_textMeshPro.text = Mathf.RoundToInt(_currentValue).ToString() + " / " + _maxValue.ToString();
    }

    public void OnChangedCurrentValue(float newValue)
    {
        //_currentValue = newValue;

        //UpdateBar();
    }

    public void OnChangedMaxValue(float newValue)
    {
        //_maxValue = newValue;

        //UpdateBar();
    }
}
