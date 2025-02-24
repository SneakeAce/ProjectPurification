using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

public abstract class Bullet : MonoBehaviour
{
    private BulletConfig _bulletConfig;
    private BulletType _bulletType;

    private Rigidbody _rigidbody;
    private MoveBullet _moveBullet;

    private int _maxCountOnScene;

    protected float _damage;
    protected Action<Bullet> _returnToPool;

    [Inject]
    private void Consturct(BulletConfig bulletConfig)
    {
        _bulletConfig = bulletConfig;

        _bulletType = _bulletConfig.BulletType;

        _maxCountOnScene = _bulletConfig.MaxCountOnScene;
    }

    public Rigidbody Rigidbody { get => _rigidbody; }
    public BulletType BulletType { get => _bulletType; }
    public int MaxCountOnScene { get => _maxCountOnScene;}

    public abstract void DamageDeal(EnemyCharacter unit);

    public void InitializeBullet(Vector3 startPoint, Quaternion rotateDirection, float distanceFlying, float bulletDamage, Action<Bullet> returnToPool)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _moveBullet = GetComponent<MoveBullet>();

        transform.position = startPoint;
        transform.rotation = rotateDirection;

        _returnToPool = returnToPool;

        _damage = bulletDamage;

        _moveBullet.Initialize(this, startPoint, distanceFlying, _bulletConfig.BulletSpeed, _returnToPool);
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("Collision == " + collision);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyCharacter target = collision.gameObject.GetComponent<EnemyCharacter>();

            Debug.Log("Bullet enter collision / target = " + target);

            DamageDeal(target);

            DeactivateBullet();
        }
    }

    private void DeactivateBullet()
    {
        _returnToPool?.Invoke(this);
    }

}
