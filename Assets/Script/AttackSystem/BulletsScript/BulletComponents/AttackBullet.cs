using UnityEngine;

public class AttackBullet
{
    private float _currentDamage;
    private LayerMask _enemyLayer;
    private Bullet _bullet;

    public void Initialize(Bullet bullet, BulletConfig config)
    {
        _bullet = bullet;
        _currentDamage = config.BaseBulletDamage;
        _enemyLayer = config.EnemyLayer;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (((1 << collider.gameObject.layer) & _enemyLayer.value) != 0)
        {
            IEnemy target = collider.gameObject.GetComponent<IEnemy>();

            Debug.Log("Bullet enter collider / target = " + target);

            DamageDeal(target);

            DestroyBullet();
        }
    }

    private void DamageDeal(IEnemy target)
    {
        target.CharacterEnemy.EnemyHealth.DamageTaken(_currentDamage);
    }

    private void DestroyBullet()
    {
        _bullet.ReturnToPool();
    }
}
