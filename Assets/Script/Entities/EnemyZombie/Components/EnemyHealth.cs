using System;

public class EnemyHealth : EntityHealth
{
    private bool _isAlive;
    private EnemyCharacter _enemy;
    private EnemyConfig _enemyConfig;

    public override event Action<IEntity> EntityDied;
    public override event Action<float> CurrentValueChanged;
    public override event Action<float> MaxValueChanged;

    public EnemyHealth(IDamageCalculator damageCalculator) : base(damageCalculator)
    {
    } 

    public bool IsAlive => _isAlive;

    public override void Initialization(IEntity entity, IEntityConfig config)
    {
        if (entity is EnemyCharacter)
            _enemy = (EnemyCharacter)entity;

        if (config is EnemyConfig)
            _enemyConfig = (EnemyConfig)config;

        _armorData = new ArmorData(_enemyConfig.HealthCharacteristics.ArmorType,
            _enemyConfig.HealthCharacteristics.ArmorValue);

        
        _maxValue = _enemyConfig.HealthCharacteristics.BaseHealthValue;
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

        UnityEngine.Debug.Log("CurrentHealth Enemy == " + _currentValue);

        if (_currentValue <= MinPossibleValue)
        {
            EntityDied?.Invoke(_enemy);
            UnityEngine.Debug.Log("Enemy is Dead");
        }
    }

}
