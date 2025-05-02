using System;

public class TurretHealth : EntityHealth
{
    public override event Action UnitDead;
    public override event Action<float> CurrentValueChanged;
    public override event Action<float> MaxValueChanged;

    public TurretHealth(IDamageCalculator damageCalculator) : base(damageCalculator)
    {
    }

    protected override void ApplyDamage(float damage)
    {
        _currentHealth -= damage;

        UnityEngine.Debug.Log("CurrentHealth Turret == " + _currentHealth);

        if (_currentHealth <= 0) 
        { 
            UnitDead?.Invoke();
            UnityEngine.Debug.Log("turret is Dead");
        }
    }
}
