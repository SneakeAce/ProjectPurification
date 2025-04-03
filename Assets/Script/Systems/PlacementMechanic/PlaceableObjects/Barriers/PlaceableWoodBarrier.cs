using UnityEngine;

public class PlaceableWoodBarrier : PlaceableObject
{
    [SerializeField] private float _damage;
    [SerializeField] private float _radiusDamage;
    [SerializeField] private float _startTimeBetweenAttack;
    [SerializeField] private LayerMask _enemyLayer;

    private float _timeBetweenAttack;

    public override void Initialization()
    {
        _timeBetweenAttack = _startTimeBetweenAttack;

        base.Initialization();
    }

    private void OnTriggerStay(Collider collider)
    {
        if (_timeBetweenAttack > 0)
        {
            _timeBetweenAttack -= Time.deltaTime;
            return;
        }

        if (collider.gameObject.layer == _enemyLayer.value && _timeBetweenAttack <= 0)
        {
            DamageDeal(collider.gameObject);
        }

    }

    private void DamageDeal(GameObject target)
    {
        if (target != null)
        {
            EnemyHealth targetHealth = target.GetComponent<EnemyHealth>();

            if (targetHealth != null)
            {
                targetHealth.DamageTaken(_damage);
            }

        }

        _timeBetweenAttack = _startTimeBetweenAttack;
    }
}
