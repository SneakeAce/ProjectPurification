using System;

public class PlaceableObjectHealth : EntityHealth
{
    private PlaceableObject _placeableObject;
    private PlaceableObjectConfig _placeableObjectconfig;

    public PlaceableObjectHealth(PlaceableObjectConfig config, IDamageCalculator damageCalculator) : base(damageCalculator)
    {
        _placeableObjectconfig = config;
    }

    public event Action<IEntity> UnitDead;
    public override event Action<float> CurrentValueChanged;
    public override event Action<float> MaxValueChanged;

    public override void Initialization(IEntity entity, IEntityConfig config)
    {
        if (entity is PlaceableObject)
            _placeableObject = (PlaceableObject)entity;

        if (config is PlaceableObjectConfig)
            _placeableObjectconfig = (PlaceableObjectConfig)config;

        _armorData = new ArmorData(_placeableObjectconfig.HealthCharacteristics.ArmorType,
            _placeableObjectconfig.HealthCharacteristics.ArmorValue);

        _maxValue = _placeableObjectconfig.HealthCharacteristics.BaseHealthValue;
        _currentValue = _maxValue;
    }

    public override void TakeDamage(DamageData damage)
    {
        throw new NotImplementedException();
    }

    protected override void ApplyDamage(float damage)
    {
        throw new NotImplementedException();
    }
}
