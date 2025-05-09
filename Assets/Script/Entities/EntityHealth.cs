using System;

public abstract class EntityHealth : IEntityHealth
{
    protected const float MinPossibleValue = 0f;

    protected float _currentValue;
    protected float _maxValue;

    protected IDamageCalculator _damageCalculator;
    protected IEntityConfig _entityConfig;

    protected ArmorData _armorData;

    public float MaxValue { get => _maxValue; set => _maxValue = value; }
    public float CurrentValue { get => _currentValue; set => _currentValue = value; }

    public abstract event Action<IEntity> EntityDied;
    public abstract event Action<float> CurrentValueChanged;
    public abstract event Action<float> MaxValueChanged;

    public EntityHealth(IDamageCalculator damageCalculator)
    {
        _damageCalculator = damageCalculator;
    }

    protected abstract void ApplyDamage(float damage);

    public abstract void Initialization(IEntity entity, IEntityConfig config);

    public abstract void TakeDamage(DamageData damage);
}
