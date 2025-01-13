using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private MoveBullet _moveBullet;

    protected float _damage;

    public Rigidbody Rigidbody { get => _rigidbody; }

    public void InitializeBullet(Vector3 startPoint, float distanceFlying, float bulletDamage)
    {
        _damage = bulletDamage;
        _moveBullet.Initialize(this, startPoint, distanceFlying);
    }

    public abstract void DamageDeal(EnemyCharacter unit);

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision == " + collision);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyCharacter target = collision.gameObject.GetComponent<EnemyCharacter>();

            Debug.Log("Bullet enter collision / target = " + target);

            DamageDeal(target);

            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
    // Код, который отвечает за столкновение с препятствиями.

}
