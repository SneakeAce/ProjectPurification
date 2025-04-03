using System;
using UnityEngine;

public abstract class PlaceableObject : MonoBehaviour
{
    [SerializeField] private BarrierConfig _barrierConfig;
    
    protected float _currentEndurance;

    public BarriersType BarrierType => _barrierConfig.ÑharacteristicsBarrier.BarrierType;
    public int MaxCountOnCurrentScene => _barrierConfig.ÑharacteristicsBarrier.MaxCountOnCurrentScene;

    public event Action<float> CurrentEnduranceChanged;
    public event Action<float> MaxEnduranceChanged;

    public virtual void Initialization()
    {
        _currentEndurance = _barrierConfig.ÑharacteristicsBarrier.MaxEndurance;
    }

    public void TakeDamage(float damage)
    {
        _currentEndurance -= damage;
    }

}
