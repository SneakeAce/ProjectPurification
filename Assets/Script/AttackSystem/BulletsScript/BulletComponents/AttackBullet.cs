using UnityEngine;

public class AttackBullet
{
    private const int SingleLayerBit = 1;
    private const int EmptyMask = 0;

    private float _currentDamage;
    private LayerMask _enemyLayer;
    private AttackType _attackType;
    private Bullet _bullet;

    public AttackBullet(Bullet bullet, BulletConfig config)
    {
        _bullet = bullet;
        _attackType = config.AttackType;
        _currentDamage = config.BaseBulletDamage;
        _enemyLayer = config.EnemyLayer;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (((SingleLayerBit << collider.gameObject.layer) & _enemyLayer.value) != EmptyMask)
        {
            IEntity target = collider.gameObject.GetComponent<IEntity>();

            Debug.Log("Bullet enter collider / target = " + target);

            DamageDeal(target); 

            DestroyBullet();
        }
    }

    private void DamageDeal(IEntity target)
    {
        IDamageable targetHealth = target.EntityHealth;

        DamageData damage = new DamageData(_attackType, _currentDamage);

        UnityEngine.Debug.Log($"DamageData = {_attackType}, {_currentDamage}");

        targetHealth.TakeDamage(damage);
    }

    private void DestroyBullet()
    {
        _bullet.ReturnToPool();
    }
}
