using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue;
    private float _currentValue;

    public event Action<float> MaxValueChanged;
    public event Action<float> CurrentValueChanged; 




}
