using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _bar;
    [SerializeField] private TMP_Text _textMeshPro;
    [SerializeField] private CharacterHealth _health;

    private float _currentValue;
    private float _maxValue;

    public void Initialize()
    {
        _maxValue = _health.MaxValue;
        _currentValue = _health.CurrentValue;

        _health.CurrentValueChanged += OnChangedCurrentValue;
        _health.MaxValueChanged += OnChangedMaxValue;

        UpdateBar();
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
    
    private void UpdateBar()
    {
        _bar.value = _currentValue / _maxValue;

        _textMeshPro.text = Mathf.RoundToInt(_currentValue).ToString();
    }

}
