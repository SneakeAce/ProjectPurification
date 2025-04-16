using UnityEngine;

public class AttackBullet
{
    private float _currentDamage;
    private LayerMask _enemyLayer;

    public void Initialize(BulletConfig config)
    {
        _currentDamage = config.BaseBulletDamage;
        _enemyLayer = config.EnemyLayer;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (((1 << collision.gameObject.layer) & _enemyLayer) != 0)
        {
            IEnemy target = collision.gameObject.GetComponent<IEnemy>();

            Debug.Log("Bullet enter collision / target = " + target);

            DamageDeal(target);

            DestroyBullet();
        }
    }

    private void DamageDeal(IEnemy target)
    {
        target.EnemyHealth.DamageTaken(_currentDamage);
    }

    private void DestroyBullet()
    {
        // Вызов метода, который вернет пулю в пул.
    }
}
