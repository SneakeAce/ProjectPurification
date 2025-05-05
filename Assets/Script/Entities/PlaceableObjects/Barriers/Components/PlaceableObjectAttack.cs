using UnityEngine;

public class PlaceableObjectAttack
{
    private PlaceableObjectConfig _config;

    private float _currentDamage;    
    private float _startTimeBetweenAttack;
    private float _timeBetweenAttack;

    private LayerMask _enemyLayer;
    private AttackType _attackType;

    public PlaceableObjectAttack(PlaceableObjectConfig config)
    {
        _config = config;

        Initialization();
    }

    private void Initialization()
    {
        _attackType = _config.AttackCharacteristics.AttackType;
        _currentDamage = _config.AttackCharacteristics.BaseDamage;
        _startTimeBetweenAttack = _config.AttackCharacteristics.BaseStartTimeBetweenAttack;
        _enemyLayer = _config.AttackCharacteristics.EnemyLayer;

        _timeBetweenAttack = _startTimeBetweenAttack;
    }

    public void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.layer == _enemyLayer.value)
        {
            DamageDeal(collider.gameObject);
        }
    }

    private void DamageDeal(GameObject target)
    {
        if (_timeBetweenAttack > 0)
        {
            _timeBetweenAttack -= Time.deltaTime;
            return;
        }

        IEnemy enemyTarget = target.GetComponent<IEnemy>();

        if (enemyTarget == null)
            return;

        DamageData damage = new DamageData(_attackType, _currentDamage);

        enemyTarget.CharacterEnemy.EnemyHealth.TakeDamage(damage);
    }

}
