using System;

public class BarrierHealth : EntityHealth
{
    private Barrier _barrier;
    private BarrierConfig _barrierConfig;

    public BarrierHealth(IDamageCalculator damageCalculator) : base(damageCalculator)
    {
    }

    public override event Action<IEntity> EntityDied;
    public override event Action<float> CurrentValueChanged;
    public override event Action<float> MaxValueChanged;

    public override void Initialization(IEntity entity, IEntityConfig config)
    {
        if (entity is Barrier)
            _barrier = (Barrier)entity;

        if (config is BarrierConfig)
            _barrierConfig = (BarrierConfig)config;

        _armorData = new ArmorData(_barrierConfig.HealthCharacteristics.ArmorType,
            _barrierConfig.HealthCharacteristics.ArmorValue);

        _maxValue = _barrierConfig.HealthCharacteristics.BaseHealthValue;
        _currentValue = _maxValue;
    }

    public override void TakeDamage(DamageData damage)
    {
        float finalDamage = _damageCalculator.CalculateDamage(damage, _armorData);

        if (finalDamage <= MinPossibleValue)
            return;

        ApplyDamage(finalDamage);
    }

    protected override void ApplyDamage(float damage)
    {
        _currentValue -= damage;

        UnityEngine.Debug.Log("CurrentHealth Barrier == " + _currentValue);

        if (_currentValue <= MinPossibleValue)
        {
            EntityDied?.Invoke(_barrier);
            UnityEngine.Debug.Log("Barrier is Dead");
        }
    }
}
