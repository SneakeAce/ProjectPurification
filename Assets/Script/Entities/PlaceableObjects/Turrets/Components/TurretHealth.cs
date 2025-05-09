using System;

public class TurretHealth : EntityHealth
{
    private TurretConfig _turretConfig;
    private Turret _turret;

    public override event Action<IEntity> EntityDied;
    public override event Action<float> CurrentValueChanged;
    public override event Action<float> MaxValueChanged;

    public TurretHealth(IDamageCalculator damageCalculator) : base(damageCalculator)
    {
    }

    public override void Initialization(IEntity entity, IEntityConfig config)
    {
        if (entity is Turret)
            _turret = (Turret)entity;

        if (config is TurretConfig)
            _turretConfig = (TurretConfig)config;

        _armorData = new ArmorData(_turretConfig.HealthCharacteristics.ArmorType, 
            _turretConfig.HealthCharacteristics.ArmorValue);

        _maxValue = _turretConfig.HealthCharacteristics.BaseHealthValue;
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

        UnityEngine.Debug.Log("CurrentHealth Turret == " + _currentValue);

        if (_currentValue <= MinPossibleValue) 
        { 
            EntityDied?.Invoke(_turret);
            UnityEngine.Debug.Log("Turret is Dead");
        }
    }

}
