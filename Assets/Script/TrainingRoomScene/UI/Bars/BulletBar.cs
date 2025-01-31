using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBar : Bar
{
    [SerializeField] protected Weapon _weapon;

    protected int _currentValue;
    protected int _maxValue;

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
