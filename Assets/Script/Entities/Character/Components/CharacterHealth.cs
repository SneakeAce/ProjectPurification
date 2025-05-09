using System;

public class CharacterHealth : EntityHealth
{
    private Character _character;
    private PlayerConfig _playerConfig;

    public CharacterHealth(IDamageCalculator damageCalculator) : base(damageCalculator)
    {
    } 

    public override event Action<IEntity> EntityDied;
    public override event Action<float> CurrentValueChanged;
    public override event Action<float> MaxValueChanged;

    public override void Initialization(IEntity entity, IEntityConfig config)
    {
        if (entity is Character)
            _character = (Character)entity;

        if (config is PlayerConfig)
            _playerConfig = (PlayerConfig)config;

        _armorData = new ArmorData(_playerConfig.HealthCharacteristics.ArmorType,
            _playerConfig.HealthCharacteristics.ArmorValue);

        _maxValue = _playerConfig.HealthCharacteristics.BaseHealthValue;
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

        UnityEngine.Debug.Log("CurrentHealth Character == " + _currentValue);

        if (_currentValue <= MinPossibleValue)
        {
            EntityDied?.Invoke(_character);
            UnityEngine.Debug.Log("Character is Dead");
        }
    }

}
