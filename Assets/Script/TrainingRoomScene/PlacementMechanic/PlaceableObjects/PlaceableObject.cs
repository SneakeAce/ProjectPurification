using System;
using UnityEngine;

public abstract class PlaceableObject : MonoBehaviour
{
    [SerializeField] protected BarrierType _barrierType;
    [SerializeField] protected float _maxEndurance;
    
    protected float _currentEndurance;

    public BarrierType BarrierType => _barrierType;

    public event Action<float> CurrentEnduranceChanged;
    public event Action<float> MaxEnduranceChanged;

    public virtual void Initialization()
    {
        _currentEndurance = _maxEndurance;
    }

    public void TakeDamage(float damage)
    {
        _currentEndurance -= damage;
    }

}
