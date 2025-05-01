using System;

public abstract class EntityHealth : IDamageable
{
    protected float _currentHealth;
    protected float _maxHealth;

    protected IDamageCalculator _damageCalculator;
    protected ArmorData _armorData;

    protected TurretConfig _turretConfig;

    public abstract event Action UnitDead;
    public abstract event Action<float> CurrentValueChanged;
    public abstract event Action<float> MaxValueChanged;

    public EntityHealth(IDamageCalculator damageCalculator)
    {
        _damageCalculator = damageCalculator;
    }

    protected abstract void ApplyDamage(float damage);

    public void Initialization(TurretConfig config)
    {
        _turretConfig = config;
        _maxHealth = config.TurretDefenseAttributes.BaseEndurance;
        _currentHealth = _maxHealth;
        _armorData = new ArmorData(config.TurretDefenseAttributes.ArmorType, config.TurretDefenseAttributes.ArmorFactor);

        DamageData damage = new DamageData(config.AttackCharacteristics.AttackType, 16f);

        TakeDamage(damage);
    }

    public void TakeDamage(DamageData damage)
    {
        float finalDamage = _damageCalculator.CalculateDamage(damage, _armorData);
        UnityEngine.Debug.Log("Final Damage: " + finalDamage);  

        if (finalDamage <= 0)
            return;

        ApplyDamage(finalDamage);
    }

}
