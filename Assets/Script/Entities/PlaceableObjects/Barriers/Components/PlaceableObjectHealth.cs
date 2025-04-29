using System;

public class PlaceableObjectHealth
{
    private PlaceableObjectConfig _config;

    private float _currentEndurance;

    public event Action<float> CurrentEnduranceChanged;
    public event Action<float> MaxEnduranceChanged;

    public PlaceableObjectHealth(PlaceableObjectConfig config)
    {
        _config = config;

        _currentEndurance = _config.Health—haracteristics.BaseEndurance;
    }

    public void TakeDamage(DamageData damageData)
    {
        
    }

}
