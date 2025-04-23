using System;

public class EnemyHealth
{
    protected float _maxValue;
    protected float _currentValue;

    private bool _isAlive;
    private EnemyCharacter _enemy;

    public float MaxValue { get => _maxValue; set => _maxValue = value; }
    public float CurrentValue { get => _currentValue; set => _currentValue = value; }

    public EnemyCharacter Enemy { get => _enemy; set => _enemy = value; }
    public bool IsAlive => _isAlive;


    public event Action<IEnemy> UnitDead;

    public void Initialize(EnemyCharacter enemy, EnemyConfig enemyConfig)
    {
        _maxValue = enemyConfig.HealthCharacteristicsEnemy.BaseHealth;
        _currentValue = _maxValue;

        _enemy = enemy;

        _isAlive = true;
    }

    public void DamageTaken(float damage)
    {
        _currentValue -= damage;

        if (_currentValue <= 0)
        {
            _isAlive = false;

            UnitDead?.Invoke(_enemy);
        }
    }

    public void AddHealth(float value)
    {
        
    }
}
