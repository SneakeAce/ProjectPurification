using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletBar : Bar
{
    private Weapon _weapon;

    [Inject]
    private void Construct(Weapon weapon)
    {
        _weapon = weapon;

        Initialize();
    }

    public override void Initialize()
    {
        _maxValue = _weapon.MaxMagazineCapacity;
        _currentValue = _weapon.CurrentMagazineCapacity;

        _weapon.MaxValueChanged += OnChangedMaxValue;
        _weapon.CurrentValueChanged += OnChangedCurrentValue;

        UpdateBar();
    }

    public override void UpdateBar()
    {
        _bar.value = _currentValue;

        _textMeshPro.text = Mathf.RoundToInt(_currentValue).ToString() + "\n / " + _maxValue.ToString();
    }

    public void OnChangedCurrentValue(int newValue)
    {
        _currentValue = newValue;

        UpdateBar();
    }

    public void OnChangedMaxValue(int newValue)
    {
        _maxValue = newValue;

        UpdateBar();
    }
}
