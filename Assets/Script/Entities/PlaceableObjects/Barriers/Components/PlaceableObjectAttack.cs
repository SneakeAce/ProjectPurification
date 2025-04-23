using UnityEngine;

public class PlaceableObjectAttack
{
    private PlaceableObjectConfig _config;

    private float _currentDamage;    private float _startTimeBetweenAttack;
    private LayerMask _enemyLayer;

    private float _timeBetweenAttack;

    public PlaceableObjectAttack(PlaceableObjectConfig config)
    {
        _config = config;

        Initialization();
    }

    private void Initialization()
    {
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

        // Поменять на DamageData.
        enemyTarget.CharacterEnemy.EnemyHealth.DamageTaken(_currentDamage);
    }

}
